using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeslizarPared : PlayerEnPared
{
    public PlayerDeslizarPared(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!SaliendoDeEstado)
        {
            player.SetVelocidadY(-playerData.velocidadDeslizar);

            if (inputAgarre && yInput == 0)
            {
                fsm.CambiarEstado(player._playerAgarrarPared);
            }
        }


    }
}
