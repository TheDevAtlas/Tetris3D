using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotate;

    private void FixedUpdate()
    {
        transform.Rotate(rotate * Time.fixedDeltaTime);
    }
}
