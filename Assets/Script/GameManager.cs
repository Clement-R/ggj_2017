using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    Dictionary<int, List<int>> validBlocks = new Dictionary<int, List<int>>();
    GridManager gridManager;

	void Start () {
        gridManager = GetComponent<GridManager>();

        validBlocks[0] = new List<int>(new int[2] { 0, 3 });
        validBlocks[1] = new List<int>(new int[3] { 0, 2, 4 });
        validBlocks[2] = new List<int>(new int[4] { 0, 1, 2, 4 });
        validBlocks[3] = new List<int>(new int[1] { 0 });
        validBlocks[4] = new List<int>(new int[2] { 2, 3 });
        validBlocks[5] = new List<int>(new int[6] { 0, 1, 2, 3, 4, 5 });
    }

    public bool isPositionValid(int x, int y, int type) {
        bool isValid = false;

        // Get type of the block under the given position
        Debug.Log(x + "_" + (y - 1));
        int underBlockType = GameObject.Find(x + "_" + (y - 1)).transform.GetChild(0).GetComponent<Block>().type;

        // Check type of block under the given position
        if(validBlocks[type].Contains(underBlockType)) {
            isValid = true;
        }

        return isValid;
    }
}
