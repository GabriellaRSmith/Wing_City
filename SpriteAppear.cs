using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAppear : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

            int num = 0;
            foreach (string str in GlobalStringVector.stringVectorArray)
        {
            num += 1;
        }
        if(num != 5){
            spriteRenderer.enabled = false;
        }
        
    }
}
