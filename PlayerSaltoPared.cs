using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaltoPared : PlayerMecanicas
{
    private int direccionSaltoPared;

    public PlayerSaltoPared(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.SaltoTerminado();
        player._playerSalto.ResetSaltos();
        player.SetVelocidadSaltoPared(playerData.velocidadSaltoPared, playerData.anguloSaltoPared, direccionSaltoPared);
        player.CheckGirar(direccionSaltoPared);
        player._playerSalto.ReducirNumSaltos();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("velocidadY", player.VelocidadActual.y);
        player.anim.SetFloat("velocidadX", Mathf.Abs(player.VelocidadActual.x));

        if(Time.time >= startTime + playerData.tiempoSaltoPared)
        {
            MecanicaTerminada = true;
        }
    }

    public void DireccionSaltoPared(bool tocandoPared)
    {
        if(tocandoPared)
        {
            direccionSaltoPared = -player.DireccionPlayer;
        }
        else
        {
            direccionSaltoPared = player.DireccionPlayer;
        }
    }
}
