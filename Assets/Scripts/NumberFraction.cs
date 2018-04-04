using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberFraction : MonoBehaviour {

    public int numerator;
    public int denomenator;

    MatchingMode matchingMode;

    private void Start()
    {
        matchingMode = FindObjectOfType<MatchingMode>();
        GetComponent<Button>().onClick.AddListener(BtnClicked);
    }

    public void BtnClicked()
    {
        matchingMode.BtnSelectNumber(this);
    }
}
