using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMecanicas : PlayerEstados
{

    protected bool MecanicaTerminada;
    private bool enSuelo;
    public PlayerMecanicas(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        enSuelo = player.CheckSuelo();
    }

    public override void Enter()
    {
        base.Enter();

        MecanicaTerminada = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (MecanicaTerminada)
        {
            if(enSuelo && player.VelocidadActual.y < 0.01f)
            {
                fsm.CambiarEstado(player._playerIdle);
            }
            else
            {
                fsm.CambiarEstado(player._playerAire);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
