using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private static LevelManager instance;

    private Transform allRectangles;

    private void Awake() {
        instance = this;

        allRectangles = GameObject.Find("AllRectangles").transform;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }
    }

    public void CheckLevelCompleted() {
        foreach (Transform rectangle in allRectangles) {
            if (!rectangle.GetComponent<CorrectColorHolder>().isColorCorrect()) {
                return;
            }
        }

        NextLevel();
    }

    private void NextLevel() {
        StartCoroutine(WaitForFade(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Restart() {
        StartCoroutine(WaitForFade(SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator WaitForFade(int sceneIndex) {
        FadeManager.GetInstance().FadeIn();
        yield return new WaitForSeconds(0.66f);
        SceneManager.LoadScene(sceneIndex);
    }

    public static LevelManager GetInstance() {
        return instance;
    }
}
