using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnAire : PlayerEstados
{
    private int xInput;
    private bool inputAgarre;
    private bool enSuelo;
    private bool jumpInput;
    private bool pararSalto;
    private bool coyoteTime;
    private bool saltando;
    private bool enPared;
    private bool enParedEspalda;
    private bool oldEnPared;
    private bool oldEnParedEspalda;
    private bool coyoteTimePared;
    private bool enBorde;

    private float startTimeCoyoteTimePared;

    public PlayerEnAire(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldEnPared = enPared;
        oldEnParedEspalda = enParedEspalda;

        enSuelo = player.CheckSuelo();
        enPared = player.CheckPared();
        enParedEspalda = player.CheckParedEspalda();
        enBorde = player.CheckBorde();

        if(enPared && !enBorde)
        {
            player._playerBorde.SetPosicionBorde(player.transform.position);
        }

        if (!coyoteTimePared && !enPared && !enParedEspalda && (oldEnPared || oldEnParedEspalda))
        {
            EmpezarCoyotePared();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enPared = false;
        enParedEspalda = false;
        oldEnPared = false;
        oldEnParedEspalda = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyote();
        CheckCoyoteTimePared();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.InputSalto;
        inputAgarre = player.InputHandler.InputAgarre;
        pararSalto = player.InputHandler.pararInputSalto;

        CheckSaltoVariable();

        //TRANSICION A ATERRIZAR
        if (enSuelo && player.VelocidadActual.y < 0.01f)
        {
            fsm.CambiarEstado(player._playerAterrizar);
        }
        //TRANSICION A BORDE
        else if (enPared && !enBorde)
        {
            fsm.CambiarEstado(player._playerBorde);
        }
        //TRANSICION A SALTO PARED
        else if (jumpInput && (enPared || enParedEspalda || coyoteTimePared))
        {
            PararCoyotePared();
            enPared = player.CheckPared();
            player._playerSaltoPared.DireccionSaltoPared(enPared);
            fsm.CambiarEstado(player._playerSaltoPared);
        }
        //TRANSICION A SALTO
        else if(jumpInput && player._playerSalto.puedeSaltar())
        {
            player.InputHandler.SaltoTerminado();
            fsm.CambiarEstado(player._playerSalto);
        }
        //TRANSICION A AGARRAR PARED
        else if (enPared && inputAgarre)
        {
            fsm.CambiarEstado(player._playerAgarrarPared);
        }
        //TRANSICION A DESLIZAR PARED
        else if(enPared && xInput == player.DireccionPlayer && player.VelocidadActual.y <= 0f)
        {
            fsm.CambiarEstado(player._playerDeslizarPared);
        }
        else
        {
            player.CheckGirar(xInput);
            player.SetVelocidadX(playerData.velocidadMovimiento * xInput);

            player.anim.SetFloat("velocidadY", player.VelocidadActual.y);
            player.anim.SetFloat("velocidadX", Mathf.Abs(player.VelocidadActual.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyote()
    {
        if(coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player._playerSalto.ReducirNumSaltos();
        }
    }

    private void CheckSaltoVariable()
    {
        if (saltando)
        {
            if (pararSalto)
            {
                player.SetVelocidadY(player.VelocidadActual.y * playerData.alturaSaltoVariable);
                saltando = false;
            }
            else if (player.VelocidadActual.y <= 0f)
            {
                saltando = false;
            }
        }
    }

    private void CheckCoyoteTimePared()
    {
        if (coyoteTimePared && Time.time > startTimeCoyoteTimePared + playerData.coyoteTime)
        {
            coyoteTimePared = false;
        }
    }

    public void EmpezarCoyote() => coyoteTime=true;

    public void EmpezarCoyotePared()
    {
        coyoteTimePared = true;
        startTimeCoyoteTimePared = Time.time;
    }
    public void PararCoyotePared() => coyoteTimePared = false;

    public void Saltando() => saltando=true;

}
