using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private LayerMask enemiesMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int collisionLayer = collision.gameObject.layer;
        if (((1 << collisionLayer) & enemiesMask) != 0)
        {
            Debug.Log("End Game");
            Time.timeScale = 0f;
        }
    }
}
