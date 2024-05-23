using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    public float rows = 5;
    public float columns = 5;
    public GameObject cubePrefab;
    public float delayBetweenSpawns = 0.1f; // Delay between spawns in seconds

    void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < rows; z++)
            {
                for (int y = 0; y < columns; y++)
                {
                    // Instantiate the cube at the appropriate grid position
                    GameObject cube = Instantiate(cubePrefab, new Vector3(x - (rows / 2f) + 0.5f, y - (columns / 2f) + 0.5f, z - (rows / 2f) + 0.5f), Quaternion.identity);
                    StartCoroutine(ScaleCube(cube, 0.6f));

                    
                }
                yield return new WaitForSeconds(delayBetweenSpawns); // Wait before spawning the next cube
            }
        }
    }

    IEnumerator ScaleCube(GameObject cube, float duration)
    {
        float time = 0;
        Vector3 initialScale = Vector3.zero;
        Vector3 finalScale = new Vector3(0.9f, 0.9f, 0.9f);
        finalScale = Vector3.one;

        while (time < duration)
        {
            float t = time / duration;
            // Smoothstep interpolation for ease-in-and-out effect
            t = t * t * (3f - 2f * t);
            cube.transform.localScale = Vector3.Lerp(initialScale, finalScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        cube.transform.localScale = finalScale; // Ensure it ends at the correct scale
    }
}
