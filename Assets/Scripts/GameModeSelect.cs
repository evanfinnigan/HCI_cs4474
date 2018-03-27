using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelect : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject instructionCanvas;
    public GameObject mainCanvas;

    private void Awake()
    {
        ShowStartScreen showStart = FindObjectOfType<ShowStartScreen>();
        if (showStart != null)
        {
            if (showStart.started)
            {
                mainCanvas.SetActive(true);
                startCanvas.SetActive(false);
            }
        }
    }

    public void BtnStartGame()
    {
        mainCanvas.SetActive(true);
        startCanvas.SetActive(false);

        ShowStartScreen showStart = FindObjectOfType<ShowStartScreen>();
        if (showStart != null)
        {
            showStart.started = true;
        }
    }

    public void BtnPractice()
    {
        // Practice button logic
        SceneManager.LoadScene("PracticeMenu");
    }

    public void BtnMatch()
    {
        // Match Game button Logic
        SceneManager.LoadScene("MatchMenu");
    }

    public void BtnEqual()
    {
        // Are We Equal button logic
        SceneManager.LoadScene("Are We Equal Menu");
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
