using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM
{

    public PlayerEstados EstadoActual { get; private set; }

    public void Inicializar(PlayerEstados estadoInicial)
    {
        EstadoActual = estadoInicial;
        EstadoActual.Enter();
    }

    public void CambiarEstado(PlayerEstados nuevoEstado)
    {
        EstadoActual.Exit();
        EstadoActual = nuevoEstado;
        EstadoActual.Enter();
    }

}
