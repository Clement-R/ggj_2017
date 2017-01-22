using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	void Start () {
        Button playButton = transform.FindChild("PlayButton").GetComponent<Button>();
        Button exitButton = transform.FindChild("ExitButton").GetComponent<Button>();
        Button creditsButton = transform.FindChild("CreditsButton").GetComponent<Button>();

        playButton.onClick.AddListener(Play);
        exitButton.onClick.AddListener(Exit);
        creditsButton.onClick.AddListener(Credits);

        AkSoundEngine.PostEvent("Play_menu_loop", gameObject);
    }

    void Play() {
        AkSoundEngine.PostEvent("Play_Select_button", gameObject);
        AkSoundEngine.PostEvent("Stop_menu_loop", gameObject);
        SceneManager.LoadScene("main");
    }

    void Credits() {
        AkSoundEngine.PostEvent("Play_Select_button", gameObject);
        AkSoundEngine.PostEvent("Stop_menu_loop", gameObject);
        SceneManager.LoadScene("credits");
    }

    void Exit() {
        AkSoundEngine.PostEvent("Play_Select_button", gameObject);
        Application.Quit();
    }
}
