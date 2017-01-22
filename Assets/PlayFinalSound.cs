using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFinalSound : MonoBehaviour {
    GameManager gameManager;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    void PlaySound() {
        if (!gameManager.isListeningToCurrent && !gameManager.isListeningToFinal) {
            // Tell the game manager that the player is listening to the targeted result
            gameManager.isListeningToFinal = true;

            // Activate all RTPCs that aren't already enabled
            foreach (var rtpc in gameManager.rtpcs) {
                AkSoundEngine.SetRTPCValue("FX" + rtpc.Key, 0.25f);
            }

            // Listen to the sound and launch coroutine to unmute and make all RTPCs in previous state
            AkSoundEngine.PostEvent("Unmute_lead", gameObject);
            StartCoroutine("muteLead");
        }
    }

    IEnumerator muteLead() {
        yield return new WaitForSeconds(5f);
        AkSoundEngine.PostEvent("Mute_lead", gameObject);
        yield return new WaitForSeconds(1f);

        // Deactivate all RTPCs
        foreach (var rtpc in gameManager.rtpcs) {
            // AkSoundEngine.GetRTPCValue("FX" + rtpc.Value);
            AkSoundEngine.SetRTPCValue("FX" + rtpc.Key, 0.75f);
        }

        gameManager.isListeningToFinal = false;
    }
}
