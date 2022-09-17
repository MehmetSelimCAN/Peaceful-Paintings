using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectColorHolder : MonoBehaviour {

    [SerializeField] private Color correctColor;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool isColorCorrect() {
        Color currentColor = spriteRenderer.color;

        if (currentColor == correctColor) {
            return true;
        }

        return false;
    }

}
