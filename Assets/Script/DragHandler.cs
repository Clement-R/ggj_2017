using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static GameObject itemBeingDragged;
    public Vector3 startPosition;
    public bool dragIsValid = true;
    Transform startParent;
    GridManager god;

    void Start() {
        god = GameObject.Find("GameManager").GetComponent<GridManager>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // Check if the movement is valid
        if (transform.parent.tag != "Inventory") {
            string[] name = transform.parent.name.Split('_');
            int x = Int32.Parse(name[0]);
            int y = Int32.Parse(name[1]);

            dragIsValid = god.canMove(x, y);
        }

        if (dragIsValid) {
            itemBeingDragged = gameObject;
            startPosition = transform.position;
            startParent = transform.parent;
            
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if(dragIsValid) {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent != startParent) {

            if (startParent.tag != "Inventory") {
                string[] name = startParent.name.Split('_');
                int x = Int32.Parse(name[0]);
                int y = Int32.Parse(name[1]);

                god.emptyCell(x, y);
            }
        }
        else {
            transform.position = startPosition;
        }
    }
}
