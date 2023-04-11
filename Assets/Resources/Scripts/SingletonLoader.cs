using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLoader : MonoBehaviour
{
    [SerializeField]
    public GameObject HelixJumpPrefab;

    private void Awake()
    {
        if (GameSingleton.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instantiate(HelixJumpPrefab);
        }
    }
}
