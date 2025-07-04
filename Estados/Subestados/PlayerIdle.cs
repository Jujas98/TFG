using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerEnSuelo
{
    public PlayerIdle(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocidadX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(XInput != 0 && !SaliendoDeEstado)
        {
            fsm.CambiarEstado(player._playerMovimiento);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
