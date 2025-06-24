using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscalarPared : PlayerEnPared
{
    public PlayerEscalarPared(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!SaliendoDeEstado)
        {
            player.SetVelocidadY(playerData.velocidadEscalada);

            if (yInput != 1)
            {
                fsm.CambiarEstado(player._playerAgarrarPared);
            }
        }


    }
}
