using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    public CinemachineVirtualCamera vcam;


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
    private Vector3 velocity;
    private bool changed_follow = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.moverBola)
        {
            float speedX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float speedY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            Vector3 posicion = transform.position;
            ElShader.instance.movingBallMode = true;
            transform.position = new Vector3(speedX + posicion.x, speedY + posicion.y, posicion.z);
        } else
        {
            ElShader.instance.movingBallMode = false;

        }
        int look = -1;
        if (PlayerController.instance.lookingLeft)
        {
            look = 1;
        }
        Vector3 target = PlayerController.instance.transform.position + new Vector3(-1f * look * 0.8f, 0.8f, 0f);
        target = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.08f);
        float x = Camera.main.WorldToScreenPoint(transform.position).x;
        float y = Camera.main.WorldToScreenPoint(transform.position).y;
        ElShader.instance.centerX[0] = x;
        ElShader.instance.centerY[0] = y;
        float wiggleSpeed = 4.0f;
        float wiggleDistance = 0.005f;
        if (PlayerController.instance.bola_reached[0])
        {
            if (PlayerController.instance.moverJugador)
            {
                transform.position = target;
            }
            transform.localPosition = transform.localPosition + new Vector3(Mathf.Sin(Time.time * wiggleSpeed + 21.0f) * wiggleDistance, Mathf.Sin(Time.time * wiggleSpeed) * wiggleDistance, 0);
            if (!changed_follow)
            {
                ((CapsuleCollider2D)gameObject.GetComponent(typeof(CapsuleCollider2D))).isTrigger = false;
                ((CapsuleCollider2D)gameObject.GetComponent(typeof(CapsuleCollider2D))).enabled = false;
                changed_follow = true;
                vcam.Follow = transform;
                vcam.LookAt = transform;
            }
            // Return to player
            if (Input.GetButtonDown("Fire2"))//Configurar el mando y teclado
            {
                PlayerController.instance.animator.SetBool("LookingUp", false);
                StartCoroutine(Tween(target));
            }
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
}
