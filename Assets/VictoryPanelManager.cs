using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryPanelManager : MonoBehaviour {

    void Start () {
        Button b1 = GameObject.Find("HomeButton").GetComponent<Button>();
        b1.onClick.AddListener(GoHome);

        Button b2 = GameObject.Find("NextButton").GetComponent<Button>();
        b2.onClick.AddListener(NextLevel);
    }
	
	void NextLevel() {
        AkSoundEngine.PostEvent("Stop_All", gameObject);
        int levelIndex = int.Parse(SceneManager.GetActiveScene().name.Split('_')[1]) + 1;
        Debug.Log(levelIndex);
        if(levelIndex <= 5) {
            SceneManager.LoadScene("level_" + levelIndex);
        } else {
            SceneManager.LoadScene("credits");
        }
    }

    void GoHome() {
        AkSoundEngine.PostEvent("Stop_All", gameObject);
        SceneManager.LoadScene("start");
    } 
}
