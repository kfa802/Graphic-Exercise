using UnityEngine;

public class Transformations : MonoBehaviour
{
    CreateGrid createGrid;

    Vector3[] startPos;

    Vector3 startScale;

    // Random starting positions for the distortion
    float[] randomOffsets;


    [Header("Movement Controls")]
    public float moveFrequency = 2f;
    public float moveAmplitude = 1f;
    public float moveOffset = 0f;


    [Header("Scale Controls")]
    public float scaleFrequency = 2f;
    public float scaleAmplitude = 0.5f;
    public float scaleOffset = 1f;


    [Header("Rotation Controls")]
    public float rotationSpeed = 50f;


    [Header("Color Controls")]
    public float colorSpeed = 2f;


    [Header("Distortion Controls")]
    public float distortionSpeed = 2f;
    public float distortionAmount = 1f;
    public float randomStartRange = 10f;


    void Start()
    {
        createGrid = GetComponent<CreateGrid>();

        startPos = createGrid.startPos;

        startScale = createGrid.grid[0].localScale;

        // Create a random offset for every cube
        randomOffsets = new float[createGrid.grid.Length];

        for (int i = 0; i < randomOffsets.Length; i++)
        {
            randomOffsets[i] = Random.Range(0f, randomStartRange);
        }
    }


    void Update()
    {
        for (int i = 0, z = 0; z < createGrid.gridResolution; z++)
        {
            for (int y = 0; y < createGrid.gridResolution; y++)
            {
                for (int x = 0; x < createGrid.gridResolution; x++, i++)
                {
                    Transform cube = createGrid.grid[i];


                    // -----------------------------
                    // MOVEMENT
                    // -----------------------------

                    float movement = Mathf.Sin(
                        Time.time * moveFrequency
                        + moveOffset
                        + randomOffsets[i]
                    ) * moveAmplitude;

                    cube.localPosition =
                        startPos[i] + Vector3.up * movement;


                    // -----------------------------
                    // COS SCALING
                    // -----------------------------

                    float scale = Mathf.Cos(
                        Time.time * scaleFrequency
                        + randomOffsets[i]
                    ) * scaleAmplitude
                    + scaleOffset;

                    cube.localScale = startScale * scale;


                    // -----------------------------
                    // DIFFERENT ROTATION DIRECTIONS
                    // -----------------------------

                    float direction;

                    if (i % 2 == 0)
                    {
                        direction = 1f;
                    }
                    else
                    {
                        direction = -1f;
                    }

                    cube.Rotate(
                        Vector3.forward
                        * direction
                        * rotationSpeed
                        * Time.deltaTime
                    );


                    // -----------------------------
                    // RANDOM COLOR EVERY FRAME
                    // -----------------------------

                    float red = Random.Range(0f, 1f);
                    float green = Random.Range(0f, 1f);
                    float blue = Random.Range(0f, 1f);

                    cube.GetComponent<MeshRenderer>().material.color =
                        new Color(red, green, blue);


                    // -----------------------------
                    // TOP TO BOTTOM DISTORTION
                    // -----------------------------

                    float wave = Mathf.Sin(
                        Time.time * distortionSpeed
                        - y * 0.7f
                        + randomOffsets[i]
                    );

                    float distortion =
                        wave * distortionAmount;


                    // Move the cube randomly on X and Z
                    cube.localPosition += new Vector3(
                        distortion,
                        0f,
                        distortion
                    );
                }
            }
        }
    }
}