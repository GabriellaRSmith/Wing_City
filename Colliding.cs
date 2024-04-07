//drew inspiration from a Flappy Bird tutorial
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Colliding : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " starts colliding with us");
        Debug.Log(this.tag);


        if (collision.gameObject.name == "Capy")
        {
            SceneManager.LoadScene("City");
        }
        

    }
}
