using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAppear : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

            int num = 0;
            foreach (string str in GlobalStringVector.stringVectorArray)
        {
            num += 1;
        }
        if(num != 5){
            meshRenderer.enabled = false;
        }
        
    }

}
