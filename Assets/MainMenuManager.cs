using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	void Start () {
        Button playButton = transform.FindChild("PlayButton").GetComponent<Button>();
        Button exitButton = transform.FindChild("ExitButton").GetComponent<Button>();
        Button creditsButton = transform.FindChild("CreditsButton").GetComponent<Button>();

        playButton.onClick.AddListener(PlaySound);
        exitButton.onClick.AddListener(PlaySound);
        creditsButton.onClick.AddListener(PlaySound);
    }

    void PlaySound() {

    }
}
