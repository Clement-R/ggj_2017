using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCurrentSound : MonoBehaviour {
    public Sprite isPlaying;
    public Sprite isWaiting;

    GameManager gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);

        AkSoundEngine.PostEvent("Mute_lead", gameObject);
        AkSoundEngine.PostEvent("Play_track" + gameManager.levelIndex, gameObject);

        // Set image to waiting
        GetComponent<Image>().sprite = isWaiting;
    }

    void Update() {
        if (GetComponent<Image>().sprite == null) {
            GetComponent<Image>().sprite = isWaiting;
        }
    }

    void PlaySound() {
        if (!gameManager.isListeningToCurrent && !gameManager.isListeningToFinal) {
            // Set image to listening
            GetComponent<Image>().sprite = isPlaying;

            // Play feedback sound
            AkSoundEngine.PostEvent("Play_play_song", gameObject);

            gameManager.isListeningToCurrent = true;
            AkSoundEngine.PostEvent("Unmute_lead", gameObject);
            StartCoroutine("muteLead");
        }
    }

    IEnumerator muteLead() {
        yield return new WaitForSeconds(5f);
        AkSoundEngine.PostEvent("Mute_lead", gameObject);
        gameManager.isListeningToCurrent = false;

        // Set image to waiting
        GetComponent<Image>().sprite = isWaiting;

        // Play feedback sound
        AkSoundEngine.PostEvent("Play_Stop_song", gameObject);
    }
}
