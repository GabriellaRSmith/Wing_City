using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    

    public float moveSpeed = 5f; // Speed at which the player moves
    private float dirx, diry;

    

    void Update()
    {
        // Get input
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        //to stop the inversions
        moveV *= -1;
        moveH *= -1;

        // To finally get it to move
        Vector3 movement = new Vector3(moveH, 0f, moveV);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
