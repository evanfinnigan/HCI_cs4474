using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EqualityMode : MonoBehaviour {

    public Image fractionImage;
    public Text fractionText;

    public int maxNumerator;
    public int maxDenomenator;

    int pizzaNumerator;

    int numerator;
    int denomenator;

	// Use this for initialization
	void Start () {
        GenerateFraction();
	}
	
    public void GenerateFraction()
    {
        denomenator = Random.Range(1, maxDenomenator);
        numerator = Random.Range(1, maxNumerator > denomenator ? denomenator : maxNumerator);

        if (Random.Range(0f,1f) > 0.5f)
        {
            pizzaNumerator = numerator;
        }
        else
        {
            Random.Range(1, maxNumerator > denomenator ? denomenator : maxNumerator);
        }

        fractionText.text = "" + numerator + "/" + denomenator;
        fractionImage.fillAmount = ((float)pizzaNumerator) / denomenator;
    }

	public void BtnYes()
    {
        if (pizzaNumerator == numerator)
        {
            Debug.Log("Correct!");
            GenerateFraction();
        }
        else
        {
            Debug.Log("Incorrect! Try Again!");
        }
    }

    public void BtnNo()
    {
        if (pizzaNumerator != numerator)
        {
            Debug.Log("Correct!");
            GenerateFraction();
        }
        else
        {
            Debug.Log("Incorrect! Try Again!");
        }
    }

    public void BtnMatchMenu()
    {
        SceneManager.LoadScene("MatchMenu");
    }

    public void BtnGameSelect()
    {
        SceneManager.LoadScene("GameModeSelect");
    }
}

