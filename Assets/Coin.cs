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

    // void Update() 
    // {
    //     float distance = Vector2.Distance(PlayerBody.transform.position, transform.position);
    //     Debug.Log("Distance: " + distance);
    //     if (distance < 1.0f)
    //     {
    //         OnTouch();
    //     }
    // }

    void OnCollisionEnter2D(Collision2D col) 
    {
        GameObject touched = col.gameObject;
        if (touched == PlayerBody) 
        {
            OnTouch();
        }
    }

    
}
