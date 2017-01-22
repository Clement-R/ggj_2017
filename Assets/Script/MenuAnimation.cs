using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour {
    public Sprite[] menu;

    int index = 0;
    SpriteRenderer menuImage;

	void Start () {
        menuImage = GetComponent<SpriteRenderer>();
        StartCoroutine("LaunchAnimation");
    }

    IEnumerator LaunchAnimation() {
        yield return new WaitForSeconds(0.3f);

        menuImage.sprite = menu[index];
        index++;
        if(index > menu.Length - 1) {
            index = 0;
        }

        StartCoroutine("LaunchAnimation");
    }
}
