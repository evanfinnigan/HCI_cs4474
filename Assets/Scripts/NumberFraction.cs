using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberFraction : MonoBehaviour {

    public int numerator;
    public int denomenator;

    MatchingMode matchingMode;
    // MatchingModeIntermediate matchingModeIntermediate;
    MatchingModeExpert matchingModeExpert;

    private void Start()
    {
        matchingMode = FindObjectOfType<MatchingMode>();
        matchingModeExpert = FindObjectOfType<MatchingModeExpert>();
        GetComponent<Button>().onClick.AddListener(BtnClicked);
    }

    public void BtnClicked()
    {
        if (matchingMode)
            matchingMode.BtnSelectNumber(this);

        if (matchingModeExpert)
            matchingModeExpert.BtnSelectNumber(this);
    }
}
