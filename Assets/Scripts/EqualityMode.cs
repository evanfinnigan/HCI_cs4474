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

    public GameObject linePrefab;
    public Transform lineParent;
    public List<GameObject> lines;

	// Use this for initialization
	void Start () {
        lines = new List<GameObject>();
        GenerateFraction();
	}
	
    public void GenerateFraction()
    {
        ClearLines();

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

        GenerateLines();
    }

    public void GenerateLines()
    {
        if (denomenator == 1)
        {
            return;
        }

        for (int i = 0; i < denomenator; i++)
        {
            GameObject line = Instantiate(linePrefab, lineParent);
            lines.Add(line);

            float zRot = i * 360f / denomenator;

            line.transform.Rotate(new Vector3(0, 0, zRot));
        }
    }

    public void ClearLines()
    {
        foreach (GameObject line in lines.ToArray())
        {
            lines.Remove(line);
            Destroy(line);
        }
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
        SceneManager.LoadScene("Game Mode Select");
    }
}

