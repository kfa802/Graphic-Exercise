using UnityEngine;
using System.Collections.Generic;

public class RayCastManipulator : MonoBehaviour
{
    public int numRaycasts = 10;

    CreateGrid createGrid;

    List<GameObject> hitCubes = new List<GameObject>();


    void Start()
    {
        createGrid = GetComponent<CreateGrid>();
    }


    void Update()
    {
        // When all cubes have been hit,
        // turn them all back on
        if (hitCubes.Count >= createGrid.grid.Length)
        {
            foreach (GameObject cube in hitCubes)
            {
                cube.SetActive(true);
            }

            hitCubes.Clear();
        }


        Vector3 cubeCenter =
            createGrid.grid[0].localPosition
            + new Vector3(
                createGrid.gridResolution / 2f,
                createGrid.gridResolution / 2f,
                createGrid.gridResolution / 2f
            );


        for (int i = 0; i < numRaycasts; i++)
        {
            // Create a random direction
            Vector3 direction = Random.onUnitSphere;


            // RaycastAll detects EVERY object
            // the ray passes through
            RaycastHit[] hits = Physics.RaycastAll(
                cubeCenter,
                direction,
                100f
            );


            foreach (RaycastHit hit in hits)
            {
                GameObject hitObject =
                    hit.transform.gameObject;


                // Make sure the same cube isn't
                // added to the list multiple times
                if (!hitCubes.Contains(hitObject))
                {
                    hitObject.SetActive(false);

                    hitCubes.Add(hitObject);
                }
            }


            // Shows the ray in the Scene view
            Debug.DrawRay(
                cubeCenter,
                direction * 100f,
                Color.red
            );
        }
    }
}