using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables Estado


    public PlayerFSM fsm {  get; private set; }

    [SerializeField] private PlayerData playerData;

    public PlayerIdle _playerIdle { get; private set; }
    public  PlayerMovimiento _playerMovimiento { get; private set; }
    public PlayerSalto _playerSalto { get; private set; }
    public PlayerEnAire _playerAire { get; private set; }
    public PlayerAterrizar _playerAterrizar { get; private set; }
    public PlayerDeslizarPared _playerDeslizarPared { get; private set; }
    public PlayerAgarrarPared _playerAgarrarPared { get; private set; }
    public PlayerEscalarPared _playerEscalarPared { get; private set; }
    public PlayerSaltoPared _playerSaltoPared { get; private set; }
    public PlayerBorde _playerBorde { get; private set; }
    
    
    #endregion

    #region Componentes


    public Animator anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region  Variables de Check

    [SerializeField] private Transform checkSuelo;
    [SerializeField] private Transform checkPared;
    [SerializeField] private Transform checkBorde;

    #endregion

    #region Otras Variables
    public Vector2 VelocidadActual {  get; private set; }
    public int DireccionPlayer {  get; private set; }

    

    private Vector2 workspace;
    #endregion

    #region Funciones Unity

    private void Awake()
    {
        fsm = new PlayerFSM();
        _playerIdle = new PlayerIdle(this, fsm, playerData, "idle");
        _playerMovimiento = new PlayerMovimiento(this, fsm, playerData, "movimiento");
        _playerSalto = new PlayerSalto(this, fsm, playerData, "enAire");
        _playerAire = new PlayerEnAire(this, fsm, playerData, "enAire");
        _playerAterrizar = new PlayerAterrizar(this, fsm, playerData, "aterrizar");
        _playerDeslizarPared = new PlayerDeslizarPared(this, fsm, playerData, "deslizar");
        _playerAgarrarPared = new PlayerAgarrarPared(this, fsm, playerData, "agarrar");
        _playerEscalarPared = new PlayerEscalarPared(this, fsm, playerData, "escalar");
        _playerSaltoPared = new PlayerSaltoPared(this, fsm, playerData, "enAire");
        _playerBorde = new PlayerBorde(this, fsm, playerData, "subirBorde");

    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        DireccionPlayer = 1;

        fsm.Inicializar(_playerIdle);
    }

    private void Update()
    {
        VelocidadActual = RB.velocity;
        fsm.EstadoActual.LogicUpdate();
    }

    private void FixedUpdate()
    {
        fsm.EstadoActual.PhysicsUpdate(); 
    }
    #endregion

    #region Sets

    public void SetVelocidadCero()
    {
        RB.velocity = Vector2.zero;
        VelocidadActual = Vector2.zero;
    }

    public void SetVelocidadX(float velocidad)
    {
        workspace.Set(velocidad, VelocidadActual.y);
        RB.velocity = workspace;
        VelocidadActual = workspace;
    }

    public void SetVelocidadY(float velocidad) 
    {
        workspace.Set(VelocidadActual.x, velocidad);
        RB.velocity = workspace;
        VelocidadActual = workspace;
    }

    public void SetVelocidadSaltoPared(float velocidad, Vector2 angulo, int direccion)
    {
        angulo.Normalize();
        workspace.Set(angulo.x * velocidad * direccion, angulo.y * velocidad);
        RB.velocity = workspace;
        VelocidadActual = workspace;
    }

    #endregion

    #region Checks
    public void CheckGirar(int xInput)
    {
        if (xInput != 0 && xInput != DireccionPlayer)
        {
            GirarPlayer();
        }
       
    }

    public bool CheckSuelo()
    {
        return Physics2D.OverlapCircle(checkSuelo.position, playerData.RadioCheckSuelo, playerData.Suelo);
    }

    public bool CheckPared()
    {
        return Physics2D.Raycast(checkPared.position, Vector2.right * DireccionPlayer, playerData.distanciaCheckPared, playerData.Suelo);
    }

    public bool CheckBorde()
    {
        return Physics2D.Raycast(checkBorde.position, Vector2.right * DireccionPlayer, playerData.distanciaCheckPared, playerData.Suelo);
    }

    public bool CheckParedEspalda()
    {
        return Physics2D.Raycast(checkPared.position, Vector2.right * -DireccionPlayer, playerData.distanciaCheckPared, playerData.Suelo);
    }

    #endregion

    #region Otras Funciones

    public Vector2 GetPosicionBorde()
    {
        RaycastHit2D xHit = Physics2D.Raycast(checkPared.position, Vector2.right * DireccionPlayer, playerData.distanciaCheckPared, playerData.Suelo);
        float distanciaX = xHit.distance;
        workspace.Set(distanciaX * DireccionPlayer, 0);
        RaycastHit2D yHit = Physics2D.Raycast(checkBorde.position + (Vector3)(workspace), Vector2.down * DireccionPlayer, checkBorde.position.y - checkPared.position.y, playerData.Suelo);
        float distanciaY = yHit.distance;

        workspace.Set(checkPared.position.x + (distanciaX * DireccionPlayer), checkBorde.position.y - distanciaY);

        return workspace;
    }

    private void AnimationTrigger() => fsm.EstadoActual.AnimationTrigger();

    private void AnimationFinishTrigger() => fsm.EstadoActual.AnimationFinishTrigger();
    private void GirarPlayer()
    {
        DireccionPlayer *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion


}
