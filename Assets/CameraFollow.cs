using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerBody;

    // Update is called once per frame
    // void Update()
    // {
    //     LateUpdate();
    // }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer() {
        Vector3 playerLocation = playerBody.position;
        Vector3 offset = new Vector3(0,0.5f, -10);
        Vector3 target = playerLocation + offset;
        transform.position = Vector3.Lerp(transform.position,target, 0.1f);
    }
}
