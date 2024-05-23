using System.Collections;
using UnityEngine;

public class CubeDelayMove : MonoBehaviour
{
    public float delay;
    private void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        int t = 0;

        while (t < 15)
        {
            yield return new WaitForSeconds(delay);

            transform.Translate(new Vector3(0,-1,0));

            t++;
        }
    }
}
