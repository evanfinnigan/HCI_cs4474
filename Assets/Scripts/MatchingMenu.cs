using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchingMenu : MonoBehaviour {

    public GameObject instructionCanvas;
    public GameObject mainCanvas;

    public void BtnBeginner()
    {
        // Beginner button logic
        SceneManager.LoadScene("Practice");
    }

    public void BtnIntermediate()
    {
        // Intermediate button logic
        SceneManager.LoadScene("Practice");
    }

    public void BtnExpert()
    {
        // Expert button logic
        SceneManager.LoadScene("Practice");
    }

    public void BtnReturnToGameSelect()
    {
        SceneManager.LoadScene("Game Mode Select");
    }

    public void BtnInstructions()
    {
        // Instructions button logic
        instructionCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void BtnBack()
    {
        // Back from instructions
        mainCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }
}
