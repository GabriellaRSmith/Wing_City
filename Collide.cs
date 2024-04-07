using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Collide : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " starts colliding with us");
        Debug.Log(this.tag);
        
        if(this.tag == "Destiny"){
            SceneManager.LoadScene("Destiny/ChatGPT Destiny");
        }
        else if(this.tag == "Vilu"){
            
            SceneManager.LoadScene("Vilu/ChatGPT Vilu");
        }
        else if(this.tag == "Sammy"){
            SceneManager.LoadScene("Sam/ChatGPT Sam");
        }
        else if(this.tag == "Alex"){
            SceneManager.LoadScene("Alex/ChatGPT Alex");
        }
        else if(this.tag == "Jazzy"){
            SceneManager.LoadScene("Jazzy/ChatGPT Jazzy");
        }
        
    }
}

