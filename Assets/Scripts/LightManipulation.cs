using UnityEngine;

public class LightManipulation : MonoBehaviour
{
    public GameObject pointLight;

    public float amplitude = 5.0f;
    public float frequency = 1.0f;
    public float verticalAmplitude = 2.0f;


    void Update()
    {
        float time = Time.time * frequency;


        // -----------------------------
        // FIGURE 8 MOVEMENT
        // -----------------------------

        float x =
            Mathf.Sin(time) * amplitude;

        float z =
            Mathf.Sin(time * 2f)
            * amplitude
            * 0.5f;


        // -----------------------------
        // VERTICAL MOVEMENT
        // -----------------------------

        float y =
            Mathf.Cos(time)
            * verticalAmplitude;


        pointLight.transform.position =
            new Vector3(x, y, z);


        // -----------------------------
        // LIGHT COLOR
        // -----------------------------

        float t =
            (Mathf.Sin(time) + 1f) / 2f;


        pointLight.GetComponent<Light>().color =
            Color.Lerp(
                Color.red,
                Color.blue,
                t
            );
    }
}