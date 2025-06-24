using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSalto : PlayerMecanicas
{
    private int numSaltos;
    public PlayerSalto(Player player, PlayerFSM fsm, PlayerData playerData, string animacion) : base(player, fsm, playerData, animacion)
    {
        numSaltos = playerData.numeroSaltos;
    }

    public override void Enter()
    {
        base.Enter();

        player.InputHandler.SaltoTerminado();
        player.SetVelocidadY(playerData.VelocidadSalto);
        MecanicaTerminada = true;
        numSaltos--;
        player._playerAire.Saltando();
    }

    public bool puedeSaltar()
    {
        if (numSaltos > 0)
        {
            return true;
        }
        else { return false; }
    }

    public void ResetSaltos() => numSaltos = playerData.numeroSaltos;
    

    public void ReducirNumSaltos() => numSaltos--; 
}
