using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Referencias al título, menú principal y menú de opciones (Menu2)
    public GameObject tituloTexto;
    public GameObject menuOpciones;
    public GameObject menu2; // Nuevo menú (Menu2)

    void Start()
    {
        // Establece la posición inicial del menú principal (menuOpciones)
        if (menuOpciones != null)
        {
            menuOpciones.transform.localPosition = new Vector3(0, -885, 0);
            LeanTween.moveLocal(menuOpciones, new Vector3(0, -232, 0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        }

        // Establece la posición inicial del segundo menú (Menu2)
        if (menu2 != null)
        {
            menu2.transform.localPosition = new Vector3(0, -885, 0);
        }

        // Inicia la animación del título
        if (tituloTexto != null)
        {
            // Establece la posición inicial del título
            tituloTexto.transform.localPosition = new Vector3(7, 789, 0);

            // Anima el movimiento del título con un rebote suave
            LeanTween.moveLocal(tituloTexto, new Vector3(7, 175, 0), 1f).setEase(LeanTweenType.easeOutBounce);

            // Opcional: Fade-in del texto (si necesitas que aparezca suavemente)
            LeanTween.alphaText(tituloTexto.GetComponent<RectTransform>(), 1f, 1f).setEase(LeanTweenType.easeInQuad);
        }
    }

    // Botón de inicio: Carga la escena del juego
    public void BotonInicio()
    {
        SceneManager.LoadScene("ScenaGame"); // Cambia "ScenaGame" por el nombre exacto de tu escena
    }

    // Botón de opciones: Muestra el menú de opciones (Menu2)
    public void BotonOpciones()
    {
        if (menu2 != null)
        {
            // Anima Menu2 desde la posición inicial hasta la posición final
            LeanTween.moveLocal(menu2, new Vector3(0, -232, 0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        }
    }

    // Botón atrás: Devuelve únicamente el menú de opciones (Menu2) a su posición inicial
    public void BotonAtras()
    {
        if (menu2 != null)
        {
            // Anima Menu2 de vuelta a su posición inicial
            LeanTween.moveLocal(menu2, new Vector3(0, -885, 0), 0.5f).setEase(LeanTweenType.easeInOutQuad);
        }
    }

    // Botón de salir: Cierra el juego
    public void BotonSalir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
