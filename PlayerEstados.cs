using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEstados
{
    protected Player player;
    protected PlayerFSM fsm;
    protected PlayerData playerData;

    protected bool animTerminada;

    protected bool SaliendoDeEstado;

    protected float startTime;

    private string animacion;

    public PlayerEstados(Player player, PlayerFSM fsm, PlayerData playerData, string animacion)
    {
        this.player = player;
        this.fsm = fsm;
        this.playerData = playerData;
        this.animacion = animacion;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.anim.SetBool(animacion, true);
        startTime = Time.time;
        Debug.Log(animacion);
        animTerminada = false;
        SaliendoDeEstado = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animacion, false);
        SaliendoDeEstado = true;
    }

    public virtual void LogicUpdate() 
    { 

    }

    public virtual void PhysicsUpdate() 
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => animTerminada = true;
    
}
