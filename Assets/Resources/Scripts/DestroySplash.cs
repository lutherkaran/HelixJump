using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySplash : MonoBehaviour
{
    void Start()
    {
        if (Ball.Instance.bPlayer)
        {
            GameObject.Destroy(gameObject, 2f);
        }
    }
}
