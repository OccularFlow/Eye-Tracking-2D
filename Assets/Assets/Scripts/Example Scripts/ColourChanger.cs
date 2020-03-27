using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour, IGazeableObject
{
    SpriteRenderer spriteRenderer;
    Color originalColor;
    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    public void gazeAction() {
        spriteRenderer.color = Color.black;
    }

    public void currentlyGazing() {}

    public void stoppedGazing() {
        spriteRenderer.color = originalColor;
    }
    
    public float getGazeTime() {
        return 0.1f;
    }
}
