using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Recuerdos : MonoBehaviour
{
    [SerializeField]
    GameObject canvasRecuerdo;
    [SerializeField]
    float tamañoImagen;
    [SerializeField]
    float velAnim;
    [SerializeField]
    LeanTweenType curvAnim;
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip sonidoColision; // Clip de sonido que se reproducirá


    private void Start()
    {
        canvasRecuerdo.SetActive(false);
        canvasRecuerdo.transform.localScale = Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.instance.moverJugador = false;
        PlayerBehaviour controller = GetComponent<PlayerBehaviour>();
        PlayerController.instance.AumentaNumeroRecuerdos(1);
        canvasRecuerdo.SetActive(true);
        LeanTween.scale(canvasRecuerdo, Vector2.one * tamañoImagen, velAnim).setEase(curvAnim);
        // Comprueba si el AudioSource y el clip están asignados
        if (audioSource != null && sonidoColision != null)
        {
            // Reproduce el sonido
            audioSource.PlayOneShot(sonidoColision);
        }
        else
        {
            Debug.LogWarning("AudioSource o sonidoColision no están asignados en el Inspector.");
        }

    }

    private void Update()
    {
        if (canvasRecuerdo.active && Input.GetButtonDown("Submit"))
        {
            PlayerController.instance.moverJugador = true;

            LeanTween.scale(canvasRecuerdo, Vector2.zero, velAnim).setEase(curvAnim).setOnComplete(() =>
            {
                canvasRecuerdo.SetActive(false);
            });
        }
    }
}