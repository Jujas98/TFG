using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorde : PlayerEstados
{
    private Vector2 posicionInicial;
    private Vector2 posicionBorde;
    private Vector2 startPos;
    private Vector2 stopPos;

    private bool colgando;
    private bool subirBorde;

    private int xInput;
    private int yInput;

    public PlayerBorde(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        player.anim.SetBool("escalarBorde", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        colgando = true;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocidadCero();
        player.transform.position = posicionInicial;
        posicionBorde = player.GetPosicionBorde();

        startPos.Set(posicionBorde.x - (player.DireccionPlayer * playerData.StartOffset.x), posicionBorde.y - playerData.StartOffset.y);
        stopPos.Set(posicionBorde.x + (player.DireccionPlayer * playerData.StopOffset.x), posicionBorde.y - playerData.StopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        colgando = false;

        if (subirBorde)
        {
            player.transform.position = stopPos;
            subirBorde = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (animTerminada)
        {
            fsm.CambiarEstado(player._playerIdle);
        }
        else
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;

            player.SetVelocidadCero();
            player.transform.position = startPos;

            if (xInput == player.DireccionPlayer && colgando && !subirBorde)
            {
                subirBorde = true;
                player.anim.SetBool("escalarBorde", true);
            }
            else if (yInput == -1 && colgando && !subirBorde)
            {
                fsm.CambiarEstado(player._playerAire);
            }
        }


    }

    public void SetPosicionBorde(Vector2 pos) => posicionInicial = pos;

}
