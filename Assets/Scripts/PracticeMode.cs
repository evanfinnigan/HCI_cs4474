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

    public GameObject linePrefab;
    public Transform lineParent;
    List<GameObject> lines;

    public int maxNumerator;
    public int maxDenomenator;

    int numerator;
    int denomenator;

    public void Start()
    {
        lines = new List<GameObject>();
        GenerateFraction();
    }

    private void GenerateFraction()
    {
        ClearLines(lines);

        numberInput.text = "";

        denomenator = Random.Range(1, maxDenomenator);
        numerator = Random.Range(0, maxNumerator > denomenator ? denomenator : maxNumerator);

        denomenatorText.text = "" + denomenator;
        numberText.text = "" + ((float)numerator) / denomenator;
        fractionImage.fillAmount = ((float)numerator) / denomenator;

        lines = GenerateLines(denomenator, lineParent);
    }

    public List<GameObject> GenerateLines(int denomenator, Transform lineParent)
    {
        List<GameObject> lines = new List<GameObject>();

        if (denomenator == 1)
        {
            return lines;
        }

        for (int i = 0; i < denomenator; i++)
        {
            GameObject line = Instantiate(linePrefab, lineParent);
            lines.Add(line);

            float zRot = i * 360f / denomenator;

            line.transform.Rotate(new Vector3(0, 0, zRot));
        }

        return lines;
    }

    public void ClearLines(List<GameObject> lines)
    {
        foreach (GameObject line in lines.ToArray())
        {
            lines.Remove(line);
            Destroy(line);
        }
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
