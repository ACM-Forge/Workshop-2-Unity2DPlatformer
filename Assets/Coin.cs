using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject Player; 
    public GameObject PlayerBody; 


    void OnTouch() 
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject touched = col.gameObject;
        // If 
        if (touched == PlayerBody) 
        {
            OnTouch();
        }
    }
}
