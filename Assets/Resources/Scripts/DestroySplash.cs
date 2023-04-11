using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySplash : MonoBehaviour
{
    void Start()
    {
        if (GameSingleton.Instance.ball.bAlive)
        {
            GameObject.Destroy(gameObject, 2f);
        }
    }
}
