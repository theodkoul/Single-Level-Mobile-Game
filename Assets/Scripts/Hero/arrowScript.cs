using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster1")) {
            Destroy(gameObject,0.1f);
        }
    }
    
}
