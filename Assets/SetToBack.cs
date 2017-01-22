using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToBack : MonoBehaviour {

    public RectTransform panelRectTransform;

    // Use this for initialization
    void Update () {
        panelRectTransform.SetAsLastSibling();
    }
}
