using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip clickSound; // Clip de sonido

    void Start()
    {
        // Pre-carga el sonido si está asignado
        if (audioSource != null && clickSound != null)
        {
            audioSource.clip = clickSound;
            audioSource.Play();
            audioSource.Stop();
        }
    }

    public void PlayButtonSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound); // Reproduce el sonido de inmediato
        }
        else
        {
            Debug.LogError("AudioSource o ClickSound no están asignados en el Inspector.");
        }
    }
}
