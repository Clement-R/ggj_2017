using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<int> waitedModifiers = new List<int>();
    public int levelIndex;
    public bool isListeningToFinal = false;
    public bool isListeningToCurrent = false;
    public Dictionary<int, bool> rtpcs = new Dictionary<int, bool>();
    public List<Sprite> backgrounds = new List<Sprite>();
    public List<Sprite> playingButtons = new List<Sprite>();
    public List<Sprite> waitingButtons = new List<Sprite>();
    public bool hasWin = false;
    public GameObject victoryPanel;

    Dictionary<int, List<int>> validBlocks = new Dictionary<int, List<int>>();
    GridManager gridManager;

	void Start () {
        
        GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = backgrounds[levelIndex - 1];

        GameObject.Find("FinalButton").GetComponent<PlayFinalSound>().isPlaying = playingButtons[levelIndex - 1];
        GameObject.Find("FinalButton").GetComponent<PlayFinalSound>().isWaiting = waitingButtons[levelIndex - 1];

        gridManager = GetComponent<GridManager>();

        validBlocks[0] = new List<int>(new int[3] { 0, 3, 5 });
        validBlocks[1] = new List<int>(new int[4] { 0, 2, 4, 5 });
        validBlocks[2] = new List<int>(new int[5] { 0, 1, 2, 4, 5 });
        validBlocks[3] = new List<int>(new int[2] { 0, 5 });
        validBlocks[4] = new List<int>(new int[3] { 2, 3, 5 });
        validBlocks[5] = new List<int>(new int[6] { 0, 1, 2, 3, 4, 5 });

        for (int i = 0; i < waitedModifiers.Count; i++) {
            rtpcs.Add(waitedModifiers[i], false);
        }
    }

    void Update() {
        // Check all cells in the game
        if(!isListeningToFinal) {
            // Deactivate all RTPC
            for (int i = 0; i < waitedModifiers.Count; i++) {
                rtpcs[waitedModifiers[i]] = false;
            }

            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    // Get the cell and check if it can activate modifiers
                    GameObject block = GameObject.Find(i + "_" + j);
                    SoundModifier soundManager = block.GetComponent<SoundModifier>();

                    // If it's a modifier cell we check if a block is on it and activate the RTPC if so
                    if (soundManager != null) {
                        if (block.transform.childCount > 0) {
                            Block bl = block.transform.GetChild(0).GetComponent<Block>();
                            rtpcs[bl.modifier] = true;
                        }
                    }
                }
            }
            
            // Check all modifiers and activate RTPC accordingly
            bool allRtcpActivated = true;

            foreach (var rtpc in rtpcs) {
                if (rtpc.Value) {
                    AkSoundEngine.SetRTPCValue("FX" + rtpc.Key, 0.25f);
                } else {
                    AkSoundEngine.SetRTPCValue("FX" + rtpc.Key, 0.75f);
                    allRtcpActivated = false;
                }
            }

            if (allRtcpActivated) {
                hasWin = true;
                victoryPanel.SetActive(true);
            }
        }
    }

    public bool isPositionValid(int x, int y, int type) {
        bool isValid = false;

        // Get type of the block under the given position
        int underBlockType = GameObject.Find(x + "_" + (y - 1)).transform.GetChild(0).GetComponent<Block>().type;

        // Check type of block under the given position
        if(validBlocks[type].Contains(underBlockType)) {
            isValid = true;
        }

        return isValid;
    }

    public bool isPositionModifier(int x, int y) {
        SoundModifier sd = GameObject.Find(x + "_" + y).GetComponent<SoundModifier>();

        if(sd != null) {
            return true;
        }

        return false;
    }
}
