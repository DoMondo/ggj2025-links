using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public Transform object1; // Asigna este objeto en el Inspector
    public Transform object2; // Asigna este objeto en el Inspector
    public Transform object3; // Asigna este objeto en el Inspector

    public float parallaxFactor1 = 0.02f;
    public float parallaxFactor2 = 0.04f;
    public float parallaxFactor3 = 0.06f;

    private Vector3 screenCenter;
    private Vector3 initialPosObject2;
    private Vector3 initialPosObject3;

    void Start()
    {
        screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        if (object2 != null)
            initialPosObject2 = object2.localPosition;
        if (object3 != null)
            initialPosObject3 = object3.localPosition;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 offset = mousePosition - screenCenter;

        if (object1 != null)
            object1.localPosition = new Vector3(offset.x * parallaxFactor1, offset.y * parallaxFactor1, object1.localPosition.z);

        if (object2 != null)
            object2.localPosition = initialPosObject2 + new Vector3(offset.x * parallaxFactor2, offset.y * parallaxFactor2, 0f);

        if (object3 != null)
            object3.localPosition = initialPosObject3 + new Vector3(offset.x * parallaxFactor3, offset.y * parallaxFactor3, 0f);
    }
}
