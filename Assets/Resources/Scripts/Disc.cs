using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : MonoBehaviour
{
    private float angle = 45f;
    public float speed = 1f;

    void Update()
    {
        this.transform.Rotate(0, 0, angle * speed * Time.deltaTime, Space.Self);
    }
}
