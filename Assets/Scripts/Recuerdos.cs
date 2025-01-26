using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Recuerdos : MonoBehaviour
{
    [SerializeField]
    GameObject canvasRecuerdo;
    [SerializeField]
    float tama�oImagen;
    [SerializeField]
    float velAnim;
    [SerializeField]
    LeanTweenType curvAnim;
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip sonidoColision; // Clip de sonido que se reproducir�


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
        LeanTween.scale(canvasRecuerdo, Vector2.one * tama�oImagen, velAnim).setEase(curvAnim);
        // Comprueba si el AudioSource y el clip est�n asignados
        if (audioSource != null && sonidoColision != null)
        {
            // Reproduce el sonido
            audioSource.PlayOneShot(sonidoColision);
        }
        else
        {
            Debug.LogWarning("AudioSource o sonidoColision no est�n asignados en el Inspector.");
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