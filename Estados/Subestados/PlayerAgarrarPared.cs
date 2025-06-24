using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgarrarPared : PlayerEnPared
{
    private Vector2 m_Posicion;
    public PlayerAgarrarPared(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
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
    }

    public override void Enter()
    {
        base.Enter();
        m_Posicion = player.transform.position;
        MantenerPosicion();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        

        if (!SaliendoDeEstado)
        {
            MantenerPosicion();

            if (yInput > 0)
            {
                fsm.CambiarEstado(player._playerEscalarPared);
            }
            else if (yInput < 0 || !inputAgarre)
            {
                fsm.CambiarEstado(player._playerDeslizarPared);
            }
        }
        
    }

    private void MantenerPosicion()
    {
        player.transform.position = m_Posicion;
        player.SetVelocidadX(0f);
        player.SetVelocidadY(0f);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
