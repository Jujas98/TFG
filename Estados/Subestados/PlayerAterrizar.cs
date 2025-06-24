using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAterrizar : PlayerEnSuelo
{
    public PlayerAterrizar(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!SaliendoDeEstado)
        {
            if (XInput != 0)
            {
                fsm.CambiarEstado(player._playerMovimiento);
            }
            else if (animTerminada)
            {
                fsm.CambiarEstado(player._playerIdle);
            }
        }
        

    }
}
