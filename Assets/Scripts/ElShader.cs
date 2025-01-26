using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElShader : MonoBehaviour
{
    public static ElShader instance;

    public ComputeShader shader;


    int _screenWidth;
    int _screenHeight;
    RenderTexture _renderTexture;

    [Range(0, 2000)] public int distance = 1000;
    [Range(0, 2000)] public float centerX = 1000;
    [Range(0, 2000)] public float centerY = 1000;
    [Range(0, 1)] public float grayscale;

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

    void Start()
    {
        CreateRenderTexture();
    }

    void CreateRenderTexture()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        _renderTexture = new RenderTexture(_screenWidth, _screenHeight, 24);
        _renderTexture.filterMode = FilterMode.Bilinear;
        _renderTexture.enableRandomWrite = true;
        _renderTexture.Create();
    }

    void Update()
    {
        if (Screen.width != _screenWidth || Screen.height != _screenHeight)
            CreateRenderTexture();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, _renderTexture);

        var mainKernel = shader.FindKernel("Vignette");
        shader.SetInt("_ResultWidth", _renderTexture.width);
        shader.SetInt("_ResultHeight", _renderTexture.height);
        shader.SetInt("_pixeldistance",  distance);
        shader.SetFloat("_centerX", centerX);
        shader.SetFloat("_centerY", centerY);
        shader.SetFloat("_grayscale", grayscale);

        shader.SetTexture(mainKernel, "_Result", _renderTexture);
        shader.GetKernelThreadGroupSizes(mainKernel, out uint xGroupSize, out uint yGroupSize, out _);
        shader.Dispatch(mainKernel,
            Mathf.CeilToInt(_renderTexture.width / xGroupSize),
            Mathf.CeilToInt(_renderTexture.height /yGroupSize),
            1);

        Graphics.Blit(_renderTexture, dest);
    }
}
