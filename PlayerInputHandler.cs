using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    public Vector2 InputMovimiento {  get; private set; }
    public int NormInputX {  get; private set; }
    public int NormInputY { get; private set; }
    public bool InputSalto { get; private set; }
    public bool pararInputSalto { get; private set; }
    public bool InputAgarre { get; private set; }

    [SerializeField] private float InputSaltoHoldTime = 0.2f;
    private float InputSaltoStartTime;

    private void Update()
    {
        CheckInputSalto();
    }

    public void InputMover(InputAction.CallbackContext context)
    {
        InputMovimiento = context.ReadValue<Vector2>();

        if (Mathf.Abs(InputMovimiento.x) > 0.5f)
        {
        NormInputX = (int)(InputMovimiento * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (Mathf.Abs(InputMovimiento.y) > 0.5f)
        {
            NormInputY = (int)(InputMovimiento * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY= 0;
        }


    }

    public void InputSaltar(InputAction.CallbackContext context) 
    {
        if(context.started)
        {
            InputSalto = true;
            pararInputSalto = false;
            InputSaltoStartTime = Time.time;
        }
        if(context.canceled)
        {
            pararInputSalto = true;
        }
    }

    public void InputAgarrar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InputAgarre = true;

        }
        if (context.canceled)
        {
            InputAgarre = false;
        }
    }



    public void SaltoTerminado() => InputSalto = false;

    private void CheckInputSalto()
    {
        if (Time.time >= InputSaltoStartTime + InputSaltoHoldTime)
        {
            InputSalto = false;
        }
    }
 

}
