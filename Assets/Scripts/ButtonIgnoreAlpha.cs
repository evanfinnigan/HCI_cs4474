using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIgnoreAlpha : MonoBehaviour {

    public Image buttonImage;

    private void Awake()
    {
        if (buttonImage != null)
        {
            buttonImage.alphaHitTestMinimumThreshold = 0.1f;
        }
    }
}
