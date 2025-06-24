using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimiento : PlayerEnSuelo
{
    public PlayerMovimiento(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.CheckGirar(XInput);

        player.SetVelocidadX(playerData.velocidadMovimiento * XInput);

        if(XInput == 0 && !SaliendoDeEstado)
        {
            fsm.CambiarEstado(player._playerIdle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
