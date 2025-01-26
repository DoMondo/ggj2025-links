using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoPlataformas : MonoBehaviour
{
    Rigidbody2D plataformRb;

    //Velocidad
    [SerializeField]
    float speed;
    [SerializeField]
    float distance;
    float frame = 50;
    int sign = 1;
    Vector2 initialPos;

    void Start()
    {
        //Coge el componenete del rigidbody
        plataformRb = GetComponent<Rigidbody2D>();
        //Guardar posicion inicial
        initialPos = transform.position;
        frame = Random.value * 100.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frame += sign * speed;
        if (frame >= 100)
        {
            sign = -1;
        }
        if (frame <= 0)
        {
            sign = 1;
        }
        plataformRb.transform.position = new Vector3(initialPos.x + ((frame / 50.0f) - 0.5f) * distance, initialPos.y, 0.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
