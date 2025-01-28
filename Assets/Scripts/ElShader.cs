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

    public float[] centerX;
    public float[] centerY;

    [Range(0, 1)] public float grayscale;
    public bool movingBallMode;
    public int num_elements_to_draw;

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

        shader.SetFloat("_grayscale", grayscale);
        for (int i = 0; i < 6; i++)
        {
            shader.SetFloat("_centerX" + i, centerX[i]);
            shader.SetFloat("_centerY" + i, centerY[i]);

        }
        shader.SetInt("_num_elements_to_draw", num_elements_to_draw);
        shader.SetBool("_moving_ball_mode", movingBallMode);

        shader.SetTexture(mainKernel, "_Result", _renderTexture);
        shader.GetKernelThreadGroupSizes(mainKernel, out uint xGroupSize, out uint yGroupSize, out _);
        shader.Dispatch(mainKernel,
            Mathf.CeilToInt(_renderTexture.width / xGroupSize),
            Mathf.CeilToInt(_renderTexture.height /yGroupSize),
            1);

        Graphics.Blit(_renderTexture, dest);
    }
}
