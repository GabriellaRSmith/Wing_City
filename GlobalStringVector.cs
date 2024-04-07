using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GlobalStringVector
{
    public static string[] stringVectorArray = new string[]
    {
        // "Diamond",
        // "Gold",
        // "Ruby",
        //"Emerald"
        // "Purple"
        // Add more strings as needed
    };

    public static void AddString(string newString)
{
    // Create a new array with increased size
    string[] newArray = new string[stringVectorArray.Length + 1];

    // Copy existing strings to the new array
    for (int i = 0; i < stringVectorArray.Length; i++)
    {
        newArray[i] = stringVectorArray[i];
    }

    // Add the new string to the end of the new array
    newArray[newArray.Length - 1] = newString;

    // Update the global vector with the new array
    stringVectorArray = newArray;
}

public static bool Contains(string tag)
{
    foreach (string s in stringVectorArray)
    {
        if (s == tag)
        {
            return true;
        }
    }
    return false;
}

}