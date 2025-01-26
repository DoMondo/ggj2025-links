using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMenuControllerWin : MonoBehaviour
{
    // Bot�n Inicio: Carga la escena "Menu"
    public void BotonInicio()
    {
        SceneManager.LoadScene("Menu"); 
    }

    // Bot�n Salir: Cierra el juego
    public void BotonSalir()
    {
#if UNITY_EDITOR
        // Si est�s en el editor, detiene el modo de juego
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si est�s en una build, cierra la aplicaci�n
        Application.Quit();
#endif
    }
}
