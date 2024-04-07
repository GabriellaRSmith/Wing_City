using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHide : MonoBehaviour
{
    public Button buttonToHide;
    void Start()
    {
        

            int num = 0;
            foreach (string str in GlobalStringVector.stringVectorArray)
        {
            num += 1;
        }
        if(num != 5){
            buttonToHide.gameObject.SetActive(false);
        }
        
    }
}
