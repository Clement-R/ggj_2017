using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerEnterHandler {
    public int type;
    public int modifier;

    public void OnPointerEnter(PointerEventData eventData) {
        if(transform.parent.tag == "Inventory") {
            AkSoundEngine.PostEvent("Play_Over_button", gameObject);
        }
    }
}
