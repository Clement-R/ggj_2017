using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public int[][] grid = new int[5][];
    
	void Start () {
        // Initialize grid
        for (int y = 0; y < 5; y++) {
            grid[y] = new int[5];

            for (int x = 0; x < 5; x++) {
                grid[y][x] = 9;
            }
        }
	}
	
	void Update () {
        // DEBUG
		if(Input.GetKeyDown(KeyCode.Space)) {

            string[] deb2 = new string[5];

            for (int i = 0; i < 5; i++) {
                string deb = "";
                for (int j = 0; j < 5; j++) {
                    deb = deb + grid[i][j];
                }
                deb2[i] = deb;
            }

            for (int i = 4; i >= 0; i--) {
                Debug.Log(deb2[i]);
            }
        }
        // EOF DEBUG
	}

    public bool isCellEmpty(int x, int y) {
        if(grid[y][x] == 9) {
            return true;
        }

        return false;
    }

    public bool isColumnFull(int y) {
        bool isFull = true;

        for (int i = 0; i < 5; i++) {
            if (isCellEmpty(i, y)) {
                isFull = false;
                break;
            }
        }

        return isFull;
    }

    public bool canMove(int x, int y) {
        bool canMove = true;

        if(y != 4) {
            if(!isCellEmpty(x, y +1)) {
                canMove = false;
            }
        }

        return canMove;
    }

    public KeyValuePair<int, int> getFirstEmptyCellInColumn(int x) {
        KeyValuePair<int, int> cellPosition = new KeyValuePair<int, int>();

        for (int i = 0; i < 5; i++) {
            if(isCellEmpty(x, i)) {
                cellPosition = new KeyValuePair<int, int>(x , i);
                break;
            }
        }

        return cellPosition;
    }

    public void emptyCell(int x, int y) {
        grid[y][x] = 9;
    }

    public void fillCell(int x, int y) {
        grid[y][x] = 1;
    }

    public void fillCell(int x, int y, int value) {
        grid[y][x] = value;
    }
}
