using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelect : MonoBehaviour
{

    public GameObject instructionCanvas;
    public GameObject mainCanvas;

    public void BtnPractice()
    {
        // Practice button logic
        SceneManager.LoadScene("Practice");
    }

    public void BtnMatch()
    {
        // Match Game button Logic
        SceneManager.LoadScene("Match");
    }

    public void BtnEqual()
    {
        // Are We Equal button logic
        SceneManager.LoadScene("Are We Equal");
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
