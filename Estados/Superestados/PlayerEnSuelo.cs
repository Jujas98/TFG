using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnSuelo : PlayerEstados
{
    protected int XInput;

    private bool InputSalto;
    private bool InputAgarre;

    private bool enSuelo;
    private bool enPared;

    public PlayerEnSuelo(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        enSuelo = player.CheckSuelo();
        enPared = player.CheckPared();
    }

    public override void Enter()
    {
        base.Enter();

        player._playerSalto.ResetSaltos();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = player.InputHandler.NormInputX;
        InputSalto = player.InputHandler.InputSalto;
        InputAgarre = player.InputHandler.InputAgarre;

        if(InputSalto && player._playerSalto.puedeSaltar())
        {
            fsm.CambiarEstado(player._playerSalto);
        }
        else if (!enSuelo)
        {
            player._playerAire.EmpezarCoyote();
            fsm.CambiarEstado(player._playerAire);
        }
        else if(enPared && InputAgarre) 
        {
            fsm.CambiarEstado(player._playerAgarrarPared);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
