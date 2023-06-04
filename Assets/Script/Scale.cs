using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scale : MonoBehaviour
{
    public Vector3 bigScale;
    public Vector3 smallScale;
    public float speed;
    public float scaleSpeed;
    public float scaleFactor;

    private void Update()
    {
        // Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);

        float x = Mathf.Lerp(bigScale.x, smallScale.x, scaleFactor);
        float y = Mathf.Lerp(bigScale.y, smallScale.y, scaleFactor);
        float z = Mathf.Lerp(bigScale.z, smallScale.z, scaleFactor);

        scaleFactor += Time.deltaTime * speed;

        if(scaleFactor > 1f)
        {
            speed *= -1f;
            scaleFactor = 1f;
        }
        if (scaleFactor < 0f)
        {
            speed *= -1f;
            scaleFactor = 0f;
        }


        transform.localScale = new Vector3(x, y, z);
    }
}
