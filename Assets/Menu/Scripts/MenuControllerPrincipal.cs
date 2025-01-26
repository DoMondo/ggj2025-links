using System.Collections; // Necesario para IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerPrincipal : MonoBehaviour
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

    // Botón de inicio: Carga la escena del juego después de 0.5 segundos
    public void BotonInicio()
    {
        StartCoroutine(CargarEscenaConRetraso("Nivel", 0.5f)); // Cambia "Nivel" por el nombre exacto de tu escena
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

    // Botón de salir: Cierra el juego después de 0.5 segundos
    public void BotonSalir()
    {
        StartCoroutine(CerrarJuegoConRetraso(0.5f)); // Inicia la corutina con un retraso de 0.5 segundos
    }

    // Corutina para cerrar el juego después de un retraso
    private IEnumerator CerrarJuegoConRetraso(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor
#else
        Application.Quit(); // Cierra la aplicación en la build
#endif
    }

    // Corutina para cargar la escena después de un retraso
    private IEnumerator CargarEscenaConRetraso(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        SceneManager.LoadScene(sceneName); // Carga la escena indicada
    }
}
