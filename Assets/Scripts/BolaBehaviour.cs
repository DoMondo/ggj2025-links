using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BolaBehaviour : MonoBehaviour
{
    public static BolaBehaviour instance;

    //Para las características de la bola
    public float radioBase = 5f;
    public float speed = 5f;
    public float incrementoPorColeccionable = 1f;
    public float moveDuration = 0.2f;


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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoverBolaOn();
        int look = -1;
        if (PlayerController.instance.lookingLeft)
        {
            look = 1;
        }
        Vector3 target = PlayerController.instance.transform.position + new Vector3(-1f * look * 0.5f, 0.5f, 0f);
        if (Input.GetButtonDown("Fire2"))//Configurar el mando y teclado
        {
            PlayerController.instance.animator.SetBool("LookingUp",false);//No me la hace
            StartCoroutine(Tween(target));
            BolaBehaviour.instance.MoverBolaOn();
        }
        if (PlayerController.instance.moverBola)
        {
            // Set vignetting accordingly
            float x = Camera.main.WorldToScreenPoint(transform.position).x;
            float y = Camera.main.WorldToScreenPoint(transform.position).y;
            ElShader.instance.centerX = x;
            ElShader.instance.centerY = y;
        }

    }
    float InOutQuadBlend(float t)
    {
        if (t <= 0.5f)
            return 2.0f * t * t;
        t -= 0.5f;
        return 2.0f * t * (1.0f - t) + 0.5f;
    }

    //IEnumerator return method that takes in the targets position
    IEnumerator Tween(Vector3 targetPosition)
    {
        //Obtain the previous position (original position) of the gameobject this script is attached to
        Vector3 previousPosition = gameObject.transform.position;
        //Create a time variable
        float time = 0.0f;
        do
        {
            //Add the deltaTime to the time variable
            time += Time.deltaTime;
            //Lerp the gameobject's position that this script is attached to. Lerp takes in the original position, target position and the time to execute it in
            gameObject.transform.position = Vector3.Lerp(previousPosition, targetPosition, InOutQuadBlend(time / moveDuration));
            yield return 0;
            //Do the Lerp function while to time is less than the move duration.
        } while (time < moveDuration);
        PlayerController.instance.moverBola = false;
        PlayerController.instance.moverJugador = true;
    }

    public void MoverBolaOn()
    {
        if (PlayerController.instance.moverBola)
        {
            float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float speedY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            Vector3 posicion = transform.position;
            transform.position = new Vector3(speedX + posicion.x, speedY + posicion.y, posicion.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour controller = GetComponent<PlayerBehaviour>();
        if (controller != null)
        {
            //Poner que el gameobject sea hijo del jugador
        }
    }
}
