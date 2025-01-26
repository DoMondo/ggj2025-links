using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoisesPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audios = new AudioClip[7];
    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(play());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator play()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 18));
            
            AudioSource.PlayClipAtPoint(audios[Random.Range(0, 6)], PlayerController.instance.transform.position);
        }
    }

}
