using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolasPasivasBehavior : MonoBehaviour
{
    public int bola_idx;
    public bool reached_first_it = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private Vector3 velocity;
    void Update()
    {
        float x = Camera.main.WorldToScreenPoint(transform.position).x;
        float y = Camera.main.WorldToScreenPoint(transform.position).y;
        ElShader.instance.centerX[bola_idx] = x;
        ElShader.instance.centerY[bola_idx] = y;
        if (PlayerController.instance.bola_reached[bola_idx])
        {
            if (reached_first_it)
            {
                ((CapsuleCollider2D)gameObject.GetComponent(typeof(CapsuleCollider2D))).isTrigger = false;
                ((CapsuleCollider2D)gameObject.GetComponent(typeof(CapsuleCollider2D))).enabled = false;
                reached_first_it = false;
            }
            int look = -1;
            if (PlayerController.instance.lookingLeft)
            {
                look = 1;
            }
            Vector3 target = PlayerController.instance.transform.position + new Vector3(look * 1.5f, 1.5f, 0f);
            target = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.08f);
            ElShader.instance.centerX[bola_idx] = x;
            ElShader.instance.centerY[bola_idx] = y;
            float wiggleSpeed = 4.0f;
            float wiggleDistance = 0.02f;
            transform.position = target;
            transform.localPosition = transform.localPosition + new Vector3(Mathf.Sin(Time.time * wiggleSpeed + bola_idx * 21f)
                * wiggleDistance, Mathf.Sin(Time.time * wiggleSpeed + bola_idx * 3f) * wiggleDistance, 0);
        }
    }
}
