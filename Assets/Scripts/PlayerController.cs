using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : PlayerBehaviour
{
    public static PlayerController instance;
        
    // Parámetros configurables por el diseñador
    public float velocidad = 2.5f; // Velocidad de movimiento del jugador
    public int vida = 3; // Cantidad de vida inicial del jugador
    public float fuerzaSalto = 0.01f; // Intensidad del salto
    public float fuerzaRebote = 6f; // Fuerza aplicada al jugador al recibir daño
    public float longitudRaycast = 0.1f; // Longitud del rayo para detectar el suelo
    public LayerMask capaSuelo; // Máscara que identifica los objetos considerados como suelo

    // Estados del jugador
    private bool enSuelo; // Indica si el jugador está tocando el suelo

    // Referencias a componentes
    private Rigidbody2D rb; // Componente Rigidbody2D para controlar la física del jugador
    public Animator animator; // Referencia al componente Animator para controlar las animaciones

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Método que se llama al iniciar el script
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Se obtiene y almacena el componente Rigidbody2D del jugador
        base.Start();
    }

    // Método que se llama una vez por cuadro
    new void Update()
    {
        Movimiento(); // Control del movimiento horizontal del jugador
        // Detecta si el jugador está tocando el suelo usando un Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        
        enSuelo = hit.collider != null;
        //enSuelo = rb.velocityY == 0.0f;
        if (enSuelo)
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("");
        }


        // Permite al jugador saltar si está en el suelo, no está recibiendo daño y presiona la barra espaciadora
        if (enSuelo && (Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.Space)) && moverJugador)
        {
            animator.SetBool("IsJumping", true);
            gameObject.transform.SetParent(null);
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse); // Aplica una fuerza hacia arriba para el salto
        }
        base.Update();
    }

    // Control del movimiento horizontal del jugador
    public void Movimiento()
    {
        if (moverJugador)
        {
            // Obtiene el input horizontal del jugador (teclas A/D o flechas izquierda/derecha)
            float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

            // Actualiza el parámetro de animación "movement" con la velocidad del jugador
            animator.SetFloat("Speed", Mathf.Abs(velocidadX * velocidad));

            // Cambia la dirección visual del jugador dependiendo de la dirección del movimiento
            if (velocidadX < 0 && !lookingLeft)
            {
                lookingLeft = true;
                transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z); // Invierte la escala en X para mirar a la izquierda
            }

            if (velocidadX > 0 && lookingLeft)
            {
                lookingLeft = false;
                transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z); // Invierte la escala en X para mirar a la izquierda
            }


            Vector3 posicion = transform.position;
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);

            int look = -1;
            if (lookingLeft)
            {
                look = 1;
            }
            Vector3 target = transform.position + new Vector3(-1f * look * 0.5f, 0.5f, 0f);
            BolaBehaviour.instance.transform.position = target;
            // Set vignetting accordingly
            float x = Camera.main.WorldToScreenPoint(transform.position).x;
            float y = Camera.main.WorldToScreenPoint(transform.position).y;

            ElShader.instance.centerX = x;
            ElShader.instance.centerY = y;
        }
    }

    // Método para dibujar gizmos en el editor, útil para depuración
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Cambia el color del gizmo a rojo
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast); // Dibuja la línea del Raycast hacia abajo
    }
}
