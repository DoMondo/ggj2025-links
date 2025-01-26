using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    // Referencia al botón que se animará
    public RectTransform boton;

    // Posición final del botón
    public Vector3 posicionFinal = new Vector3(734, -423, 0); // Cambia esta posición según lo necesites

    // Tiempo de retraso antes de la animación
    public float retraso = 2f;

    // Duración de la animación
    public float duracion = 1f;

    void Start()
    {
        if (boton != null)
        {
            // Establece la posición inicial del botón
            boton.localPosition = new Vector3(734, -700, 0);

            // Anima el botón hacia la posición final después de un retraso
            LeanTween.moveLocal(boton.gameObject, posicionFinal, duracion)
                .setDelay(retraso)
                .setEase(LeanTweenType.easeInOutQuad); // Animación suave y profesional
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }
    }
}
