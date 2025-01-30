using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPPlatformBehaviour : MonoBehaviour
{
    [SerializeField] float speed;

    // 0 left / 1 right
    [SerializeField] float[] bounds;

    //Private values
    float initialPos;
    int direction;
    // 0 left / 1 right
    float[] limits;

    //Components
    Rigidbody2D platformRb;

    private void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        initialPos = transform.position.x;
        limits[0] = transform.position.x - bounds[0];
        limits[1] = transform.position.x + bounds[1];
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= limits[0] || transform.position.x >= limits[1])
        {
            direction *= -1;
        }
    }
}
