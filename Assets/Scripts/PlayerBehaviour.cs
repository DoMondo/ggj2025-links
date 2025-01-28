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
    public bool lookingLeft;
    public bool metaAlcanzada = false;
    public bool tiempoAcabado = false;


    public bool[] bola_reached;

    GameObject manoAnimation;
    [SerializeField]
    float velAnim;
    [SerializeField]
    LeanTweenType curvAnim;
    [SerializeField]
    float tamañoBola;
    int light_size_idx;
    int[] light_sizes = { 0, 100, 200, 300, 400, 500, 6000, 700 };
    float[] gray_scale_values = { 1.0f, 1.0f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.0f };

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
            bola_reached[0] = true;
            light_size_idx += 1;
            ElShader.instance.num_elements_to_draw = 5;
            ElShader.instance.distance = light_sizes[light_size_idx];
            ElShader.instance.grayscale = gray_scale_values[light_size_idx];
        }
        if (collision.CompareTag("bola2"))
        {
            bola_reached[1] = true;
            light_size_idx += 1;
            ElShader.instance.distance = light_sizes[light_size_idx];
            ElShader.instance.grayscale = gray_scale_values[light_size_idx];
        }
        if (collision.CompareTag("bola3"))
        {
            bola_reached[2] = true;
            light_size_idx += 1;
            ElShader.instance.distance = light_sizes[light_size_idx];
            ElShader.instance.grayscale = gray_scale_values[light_size_idx];
        }
        if (collision.CompareTag("bola4"))
        {
            bola_reached[3] = true;
            light_size_idx += 1;
            ElShader.instance.grayscale = gray_scale_values[light_size_idx];
            ElShader.instance.distance = light_sizes[light_size_idx];
        }
        if (collision.CompareTag("bola5"))
        {
            bola_reached[4] = true;
            light_size_idx += 1;
            light_size_idx += 1;
            ElShader.instance.grayscale = gray_scale_values[light_size_idx];
            ElShader.instance.distance = light_sizes[light_size_idx];
        }
        if (collision.CompareTag("bola6"))
        {
            bola_reached[5] = true;
            light_size_idx += 1;
            light_size_idx += 1;
            ElShader.instance.grayscale = gray_scale_values[light_size_idx];
            ElShader.instance.distance = light_sizes[light_size_idx];
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
        if (metaAlcanzada == true && light_size_idx < 6 && TimeController.instance.tiempo > 0)
        {
            Debug.Log("Recargar Escena");
            SceneManager.LoadScene("Nivel");
        }

    }
}
