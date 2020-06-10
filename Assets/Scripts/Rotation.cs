using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public void FixedUpdate()
    {
        transform.Rotate(Vector3.up* Time.deltaTime*100, Space.Self);
    }
}
