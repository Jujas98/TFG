using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnPared : PlayerEstados
{
    protected bool enSuelo;
    protected bool enPared;
    protected bool inputAgarre;
    protected bool inputSalto;
    protected int xInput;
    protected int yInput;

    public PlayerEnPared(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        inputAgarre = player.InputHandler.InputAgarre;
        inputSalto = player.InputHandler.InputSalto;


        if (inputSalto)
        {
            player._playerSaltoPared.DireccionSaltoPared(enPared);
            fsm.CambiarEstado(player._playerSaltoPared);
        }
        else if (enSuelo && !inputAgarre)
        {
            fsm.CambiarEstado(player._playerIdle);
        }
        else if (!enPared || (xInput != player.DireccionPlayer && !inputAgarre))
        {
            fsm.CambiarEstado(player._playerAire);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
