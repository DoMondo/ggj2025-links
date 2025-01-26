using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    // M�todo llamado autom�ticamente cuando otro collider entra en contacto con el collider marcado como "Trigger" de este objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Destruye este objeto (la moneda) al ser recogida por el jugador
            Destroy(gameObject);
        }
    }
}
