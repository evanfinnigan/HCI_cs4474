using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeMode : MonoBehaviour
{

    public Image fractionImage;
    public InputField numberInput;
    public Text numberText;
    public Text denomenatorText;

    public int maxNumerator;
    public int maxDenomenator;

    int numerator;
    int denomenator;

    public void Start()
    {
        GenerateFraction();
    }

    private void GenerateFraction()
    {
        numberInput.text = "";

        denomenator = Random.Range(1, maxDenomenator);
        numerator = Random.Range(0, maxNumerator > denomenator ? denomenator : maxNumerator);

        denomenatorText.text = "" + denomenator;
        numberText.text = "" + ((float)numerator) / denomenator;
        fractionImage.fillAmount = ((float)numerator) / denomenator;
    }

    public void TestInput()
    {
        int userInput = int.Parse(numberInput.text);

        if (userInput == numerator)
        {
            Debug.Log("Correct!");
            GenerateFraction();
        }
        else
        {
            Debug.Log("Incorrect! Try again!");
        }
    }

    public void BtnPracticeMenu()
    {
        SceneManager.LoadScene("PracticeMenu");
    }

    public void BtnGameSelect()
    {
        SceneManager.LoadScene("Game Mode Select");
    }
}
