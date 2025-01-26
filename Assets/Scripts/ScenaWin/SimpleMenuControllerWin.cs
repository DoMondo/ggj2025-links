using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMenuControllerWin : MonoBehaviour
{
    // Botón Inicio: Carga la escena "Menu"
    public void BotonInicio()
    {
        SceneManager.LoadScene("Menu"); 
    }

    // Botón Salir: Cierra el juego
    public void BotonSalir()
    {
#if UNITY_EDITOR
        // Si estás en el editor, detiene el modo de juego
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estás en una build, cierra la aplicación
        Application.Quit();
#endif
    }
}
