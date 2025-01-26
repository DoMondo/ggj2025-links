using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Salir()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor
        #else
        Application.Quit(); // Cierra la aplicación en la build
        #endif
    }
}
