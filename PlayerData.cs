using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

public class PlayerData : ScriptableObject
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 10f;

    [Header("Salto")]

    public float VelocidadSalto = 15f;
    public int numeroSaltos = 1;

    [Header("En Aire")]

    public float coyoteTime = 0.2f;
    public float alturaSaltoVariable = 0.5f;

    [Header("Deslizar Pared")]

    public float velocidadDeslizar = 3f;

    [Header("Escalar Pared")]

    public float velocidadEscalada = 3f;

    [Header("Salto Pared")]

    public float velocidadSaltoPared = 20f;
    public float tiempoSaltoPared = 0.4f;
    public Vector2 anguloSaltoPared = new Vector2(1, 2);

    [Header("Borde")]

    public Vector2 StartOffset;
    public Vector2 StopOffset;

    [Header("Checks")]

    public float RadioCheckSuelo = 0.3f;
    public float distanciaCheckPared = 0.5f;
    public LayerMask Suelo;


}
