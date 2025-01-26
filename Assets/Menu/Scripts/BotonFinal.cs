using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    // Referencia al bot�n que se animar�
    public RectTransform boton;

    // Posici�n final del bot�n
    public Vector3 posicionFinal = new Vector3(734, -423, 0); // Cambia esta posici�n seg�n lo necesites

    // Tiempo de retraso antes de la animaci�n
    public float retraso = 2f;

    // Duraci�n de la animaci�n
    public float duracion = 1f;

    void Start()
    {
        if (boton != null)
        {
            // Establece la posici�n inicial del bot�n
            boton.localPosition = new Vector3(734, -700, 0);

            // Anima el bot�n hacia la posici�n final despu�s de un retraso
            LeanTween.moveLocal(boton.gameObject, posicionFinal, duracion)
                .setDelay(retraso)
                .setEase(LeanTweenType.easeInOutQuad); // Animaci�n suave y profesional
        }
        else
        {
            Debug.LogError("El bot�n no est� asignado en el Inspector.");
        }
    }
}
