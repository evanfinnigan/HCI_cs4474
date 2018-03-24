using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EqualityMenu : MonoBehaviour {

    public GameObject instructionCanvas;
    public GameObject mainCanvas;

    public void BtnPlay()
    {
        // Practice button logic
        SceneManager.LoadScene("Are We Equal");
    }

    public void BtnReturnToGameSelect()
    {
        // Match Game button Logic
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
