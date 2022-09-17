using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {

    private static FadeManager instance;

    private Transform fadeOut;
    private Transform fadeIn;

    private void Awake() {
        instance = this;

        fadeOut = GameObject.Find("FadeOut").transform;

        fadeIn = GameObject.Find("FadeIn").transform;
        fadeIn.gameObject.SetActive(false);
    }

    public void FadeIn() {
        fadeIn.gameObject.SetActive(true);
        fadeOut.gameObject.SetActive(false);
    }

    public static FadeManager GetInstance() {
        return instance;
    }

}
