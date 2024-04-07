using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstones : MonoBehaviour
{
    // Reference to the SpriteRenderer component
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        // Ensure spriteRenderer is not null
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the tag of this GameObject exists in the GlobalStringVector
        if (GlobalStringVector.Contains(this.tag))
        {
            Debug.Log(this.tag);
        }
        else
        {
            HideSprite();
        }
    }

    // Method to hide the sprite
    public void HideSprite()
    {
        // Disable the SpriteRenderer component to hide the sprite
        spriteRenderer.enabled = false;
    }
}
