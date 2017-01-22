using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCurrentSound : MonoBehaviour {
    GameManager gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);

        AkSoundEngine.PostEvent("Mute_lead", gameObject);
        AkSoundEngine.PostEvent("Play_track" + gameManager.levelIndex, gameObject);
    }

    void PlaySound() {
        if (!gameManager.isListeningToCurrent && !gameManager.isListeningToFinal) {
            gameManager.isListeningToCurrent = true;
            AkSoundEngine.PostEvent("Unmute_lead", gameObject);
            StartCoroutine("muteLead");
        }
    }

    IEnumerator muteLead() {
        yield return new WaitForSeconds(5f);
        AkSoundEngine.PostEvent("Mute_lead", gameObject);
        gameManager.isListeningToCurrent = false;
    }
}
