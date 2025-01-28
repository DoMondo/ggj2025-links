using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public int recuerdos;
    [SerializeField]
    int recuerdosTotales;
    [SerializeField]
    TextMeshProUGUI numeroRecuerdos;

    //Boleanos
    public bool moverBola;
    public bool moverJugador;
    public bool bola0_reached;
    public bool lookingLeft;
    public bool metaAlcanzada = false;
    public bool tiempoAcabado = false;


    //Para las bolas
    [SerializeField]
    GameObject bola1;
    [SerializeField]
    GameObject bola2;
    [SerializeField]
    GameObject bola3;
    [SerializeField]
    GameObject bola4;
    [SerializeField]
    GameObject bola5;
    [SerializeField]
    GameObject manoAnimation;
    [SerializeField]
    float velAnim;
    [SerializeField]
    LeanTweenType curvAnim;
    [SerializeField]
    float tamañoBola;
    int light_size_idx;
    int[] light_sizes = { 0, 300, 400, 500, 600, 700, 1000, 1200};
    float [] gray_scale_values= { 1.0f, 1.0f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.0f};

    public GameObject mano;

    // Start is called before the first frame update
    public void Start()
    {
        light_size_idx = 0;
        metaAlcanzada = false;
        ElShader.instance.distance = light_sizes[light_size_idx];
        ElShader.instance.grayscale = gray_scale_values[light_size_idx];
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))//Configurar el mando y teclado
        {
            PlayerController.instance.animator.SetBool("LookingUp", true);//Queda rara
            moverBola = true;
            moverJugador = false;
            PlayerController.instance.animator.SetFloat("Speed", 0.0f);
        }
        if (TimeController.instance.tiempo <= 0 && !tiempoAcabado)
        {
            tiempoAcabado = true;
            // Se acabó el tiempo
            mano = Instantiate(manoAnimation, PlayerController.instance.transform.position + new Vector3(1.0f, 2.0f, -1.0f), Quaternion.identity);
            StartCoroutine(ExecuteAfterTime(3));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Nivel");
    }

    public void AumentaNumeroRecuerdos(int amount)
    {
        recuerdos += amount;
        String text = recuerdos + "/" + recuerdosTotales;
        numeroRecuerdos.SetText(text);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bola1"))
        {
            LeanTween.scale(collision.gameObject, collision.gameObject.transform.localScale, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                //LeanTween.scale(bola1, Vector2.one*tamañoBola, velAnim);
                bola0_reached = true;
                light_size_idx += 1;
                ElShader.instance.distance = light_sizes[light_size_idx];
                ElShader.instance.grayscale= gray_scale_values[light_size_idx];
            });
        }
        if (collision.CompareTag("bola2"))
        {
            LeanTween.scale(collision.gameObject, Vector2.zero, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                //bola2.SetActive(true);
                //LeanTween.scale(bola2, Vector2.one * tamañoBola, velAnim);
                light_size_idx += 1;
                ElShader.instance.distance = light_sizes[light_size_idx];
                ElShader.instance.grayscale = gray_scale_values[light_size_idx];

            });
        }
        if (collision.CompareTag("bola3"))
        {
            LeanTween.scale(collision.gameObject, Vector2.zero, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                //bola3.SetActive(true);
                //LeanTween.scale(bola3, Vector2.one * tamañoBola, velAnim);
                light_size_idx += 1;
                ElShader.instance.distance = light_sizes[light_size_idx];
                ElShader.instance.grayscale = gray_scale_values[light_size_idx];
            });
        }
        if (collision.CompareTag("bola4"))
        {
            LeanTween.scale(collision.gameObject, Vector2.zero, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                //bola4.SetActive(true);
                //LeanTween.scale(bola4, Vector2.one * tamañoBola, velAnim);
                light_size_idx += 1;
                ElShader.instance.grayscale = gray_scale_values[light_size_idx];
                ElShader.instance.distance = light_sizes[light_size_idx];
            });
        }
        if (collision.CompareTag("bola5"))
        {
            LeanTween.scale(collision.gameObject, Vector2.zero, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                //bola5.SetActive(true);
                //LeanTween.scale(bola5, Vector2.one * tamañoBola, velAnim);
                light_size_idx += 1;
                ElShader.instance.grayscale = gray_scale_values[light_size_idx];
                ElShader.instance.distance = light_sizes[light_size_idx];
            });
        }
        if (collision.CompareTag("bola6"))
        {
            LeanTween.scale(collision.gameObject, Vector2.zero, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                light_size_idx += 1;
                ElShader.instance.grayscale = gray_scale_values[light_size_idx];
                ElShader.instance.distance = light_sizes[light_size_idx];
            });
        }
        if (collision.CompareTag("Finish"))
        {
            metaAlcanzada = true;
        }
        if (light_size_idx == 6 && TimeController.instance.tiempo > 0 && metaAlcanzada)
        {
            Debug.Log("Cambiando a escena win");
            SceneManager.LoadScene("Win"); 
        }
        if (metaAlcanzada==true && light_size_idx < 6 && TimeController.instance.tiempo > 0)
        {
            Debug.Log("Recargar Escena");
            SceneManager.LoadScene("Nivel");
        }

    }
}
