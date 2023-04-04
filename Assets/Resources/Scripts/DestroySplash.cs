using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySplash : MonoBehaviour
{
    void Start()
    {
        GameObject.Destroy(gameObject,2f);
    }
}
