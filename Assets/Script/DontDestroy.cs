using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {
    public GameObject fadeOutTexture;
    public float fadeSpeed = 0.8f;

    SpriteRenderer sr;

    void Start() {
        sr = fadeOutTexture.GetComponent<SpriteRenderer>();
    }

    public void Fade() {
        StartCoroutine("fadein");
    }

    IEnumerator fadein() {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 10);
        yield return new WaitForEndOfFrame();
        if (sr.color.a < 255) {
            StartCoroutine("fadeout");
        }
        else {
            StartCoroutine("fadein");
        }
    }

    IEnumerator fadeout() {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 10);
        yield return new WaitForEndOfFrame();

        if (sr.color.a >= 0) {
            StartCoroutine("fadeout");
        }
    }
}
