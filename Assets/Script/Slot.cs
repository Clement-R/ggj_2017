using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
    GridManager gridManager;
    GameManager gameManager;

    void Start() {
        gridManager = GameObject.Find("GameManager").GetComponent<GridManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public GameObject item
    {
        get {
            if(transform.childCount > 0) {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    
    public void OnDrop(PointerEventData eventData) {
        if(!item) {
            if(transform.tag != "Inventory") {
                // Check if the block can move
                if(eventData.pointerDrag.gameObject.GetComponent<DragHandler>().dragIsValid) {

                    bool newPositionIsValid = false;
                    int blockType = eventData.pointerDrag.gameObject.GetComponent<Block>().type;

                    // Get the name of the cell where the drop happened
                    string[] name = transform.name.Split('_');
                    int x = Int32.Parse(name[0]);
                    int y = Int32.Parse(name[1]);

                    // Get first empty cell position in the targeted column
                    KeyValuePair<int, int> cellPosition = gridManager.getFirstEmptyCellInColumn(x);

                    // Get informations about actual parent of the dragged block
                    bool comingFromGrid = false;
                    int parentX = 0;
                    int parentY = 0;
                    if (!eventData.pointerDrag.gameObject.transform.parent.name.Contains("Panel")) {
                        string[] parentName = eventData.pointerDrag.gameObject.transform.parent.name.Split('_');
                        string parentTag = eventData.pointerDrag.gameObject.transform.parent.tag;
                        parentX = Int32.Parse(parentName[0]);
                        parentY = Int32.Parse(parentName[1]);

                        comingFromGrid = true;

                        Debug.Log(eventData.pointerDrag.gameObject.transform.parent.name);
                        
                    }

                    // Check if the new position is valid with the block under
                    if (cellPosition.Value == 0) {
                        newPositionIsValid = true;
                    } else {
                        // If the position is valid in the grid
                        if (gameManager.isPositionValid(cellPosition.Key, cellPosition.Value, blockType)) {
                            if(comingFromGrid) {
                                if (x == parentX && parentY == (y - 1)) {
                                } else {
                                    newPositionIsValid = true;
                                }
                            } else {
                                newPositionIsValid = true;
                            }
                        }
                    }

                    if(newPositionIsValid) {
                        // Check if the new cell is a modifier or not and play the according sound
                        if(gameManager.isPositionModifier(cellPosition.Key, cellPosition.Value)) {
                            AkSoundEngine.PostEvent("Play_Block_ok", gameObject);
                        } else {
                            AkSoundEngine.PostEvent("Play_Poser_block", gameObject);
                        }
                        
                        // Tell the grid manager that this cell is now used and give the block type
                        gridManager.fillCell(cellPosition.Key, cellPosition.Value, blockType);

                        // Get transform of the given cell and call setParent with it as parameter
                        Transform targetedCell = GameObject.Find(cellPosition.Key + "_" + cellPosition.Value).transform;
                        DragHandler.itemBeingDragged.transform.SetParent(targetedCell);

                    } else {
                        AkSoundEngine.PostEvent("Play_Block_error", gameObject);
                    }
                } else {
                    AkSoundEngine.PostEvent("Play_Block_error", gameObject);
                }
            } else {
                DragHandler.itemBeingDragged.transform.SetParent(transform);
            }
        }
    }
}
