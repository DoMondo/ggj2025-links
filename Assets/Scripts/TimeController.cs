using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    [SerializeField]
    public float tiempo;
    float tiempoTotal = 1;//7min
    [SerializeField]
    GameObject canvasLimbo;

    //Booleanos
    bool contarTiempo;

    //Texto
    [SerializeField]
    TextMeshProUGUI tiempoEnPantalla;
    [SerializeField]
    TextMeshProUGUI tiempoEnPantallaShadow;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasLimbo.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        tiempoEnPantalla.text = $"{minutos:00}:{segundos:00}";
        tiempoEnPantallaShadow.text = $"{minutos:00}:{segundos:00}";

    }
}