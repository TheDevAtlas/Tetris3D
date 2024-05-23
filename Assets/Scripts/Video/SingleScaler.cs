using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleScaler : MonoBehaviour
{
    public float howLong;
    public float delay;


    void Start()
    {
        StartCoroutine(ScaleCube(gameObject, howLong));
    }
    IEnumerator ScaleCube(GameObject cube, float duration)
    {
        yield return new WaitForSeconds(delay);
        
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

        StartCoroutine(ScaleCube2(gameObject, howLong));
    }

    IEnumerator ScaleCube2(GameObject cube, float duration)
    {
        yield return new WaitForSeconds(delay);

        float time = 0;
        Vector3 initialScale = Vector3.one;
        Vector3 finalScale = new Vector3(0.9f, 0.9f, 0.9f);
        finalScale = new Vector3(10f, 1f, 1f);

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
        StartCoroutine(ScaleCube3(gameObject, howLong));
    }

    IEnumerator ScaleCube3(GameObject cube, float duration)
    {
        yield return new WaitForSeconds(delay);

        float time = 0;
        Vector3 initialScale = new Vector3(10f, 1f, 1f);
        Vector3 finalScale = new Vector3(0.9f, 0.9f, 0.9f);
        finalScale = new Vector3(10f, 1f, 10f);

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
