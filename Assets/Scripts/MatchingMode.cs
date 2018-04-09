using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingMode : MonoBehaviour {

    public Transform grid;

    public GameObject matchCirclePrefab;
    public GameObject matchNumberPrefab;
    public AudioSource audioCorrect;
    public AudioSource audioIncorrect;

    public GameObject linePrefab;

    public int maxNumerator;
    public int maxDenomenator;

    public List<GameObject> fractions;
    public List<GameObject> corrections;

    public static bool generating = false;

    public Button resetButton;

    CircleFraction selectedCircle;
    NumberFraction selectedNumber;

    CircleFraction prevCircle;
    NumberFraction prevNumber;

    bool circleSelected;
    bool numberSelected;

    bool prevSelCir = false;
    bool prevSelNum = false;

    public Text levelText;
    int level;

    private void Start()
    {
        level = 1;
        levelText.text = "Level " + level;

        circleSelected = false;
        numberSelected = false;
        fractions = new List<GameObject>();
        GenerateFractions();
    }

    public void BtnReset()
    {
        level = 1;
        levelText.text = "Level " + level;
        GenerateFractions();
    }

    public void BtnSelectCircle(CircleFraction circle)
    {
        if (circleSelected)
        {
            if (prevSelCir)
            {
                prevNumber.gameObject.GetComponent<Image>().color = Color.white;

                prevCircle.gameObject.GetComponent<Image>().color = Color.white;
            }
            selectedCircle.gameObject.GetComponent<Image>().color = Color.white;
        }

        if (circleSelected && selectedCircle == circle)
        {
            if (prevSelCir)
            {
                prevNumber.gameObject.GetComponent<Image>().color = Color.white;

                prevCircle.gameObject.GetComponent<Image>().color = Color.white;
            }
            circleSelected = false;
            selectedCircle = null;
        }
        else
        {
            if (prevSelCir)
            {
                prevNumber.gameObject.GetComponent<Image>().color = Color.white;

                prevCircle.gameObject.GetComponent<Image>().color = Color.white;
            }
            circleSelected = true;
            selectedCircle = circle;
            selectedCircle.gameObject.GetComponent<Image>().color = Color.yellow;
        }

        TestSelected();
    }

    public void BtnSelectNumber(NumberFraction number)
    {
        if (numberSelected)
        {
            if (prevSelNum)
            {
                prevCircle.gameObject.GetComponent<Image>().color = Color.white;

                prevNumber.gameObject.GetComponent<Image>().color = Color.white;
            }
            selectedNumber.gameObject.GetComponent<Image>().color = Color.white;
        }

        if (numberSelected && selectedNumber == number)
        {
            if (prevSelNum)
            {
                prevCircle.gameObject.GetComponent<Image>().color = Color.white;

                prevNumber.gameObject.GetComponent<Image>().color = Color.white;
            }
            numberSelected = false;
            selectedNumber = null;

        }
        else
        {
            if (prevSelNum)
            {
                prevCircle.gameObject.GetComponent<Image>().color = Color.white;

                prevNumber.gameObject.GetComponent<Image>().color = Color.white;
            }
            numberSelected = true;
            selectedNumber = number;
            selectedNumber.gameObject.GetComponent<Image>().color = Color.yellow;
        }

        TestSelected();
    }
    
    public void TestSelected()
    {
        if (circleSelected && numberSelected)
        {
            
            if (selectedCircle.numerator == selectedNumber.numerator && selectedCircle.denomenator == selectedNumber.denomenator
                ||
                Mathf.Abs(((float)selectedCircle.numerator / selectedCircle.denomenator) - ((float)selectedNumber.numerator / selectedNumber.denomenator)) < 0.001)
                {
                Debug.Log("Correct!");
                audioCorrect.Play();
                selectedCircle.gameObject.GetComponent<Button>().interactable = false;
                selectedCircle.gameObject.transform.Find("Fraction Image").gameObject.SetActive(false);
                selectedCircle.gameObject.transform.Find("Correct").gameObject.SetActive(true);
                selectedCircle.gameObject.GetComponent<Image>().color = Color.green;

                selectedNumber.gameObject.GetComponent<Button>().interactable = false;
                selectedNumber.gameObject.transform.Find("Correct").gameObject.SetActive(true);
                selectedNumber.gameObject.GetComponent<Image>().color = Color.green;

                selectedCircle = null;
                selectedNumber = null;
                circleSelected = false;
                numberSelected = false;
                prevSelCir = false;
                prevSelNum = false;
                bool done = true;
                foreach (Button button in grid.GetComponentsInChildren<Button>())
                {
                    if (button.interactable)
                    {
                        done = false;
                        break;
                    }
                }
                if (done)
                {
                    level++;
                    levelText.text = "Level " + level;
                    GenerateFractions();
                }
            }
            else
            {
                Debug.Log("Incorrect! Try again!");

                audioIncorrect.Play();
                selectedCircle.gameObject.GetComponent<Image>().color = Color.red;
                selectedNumber.gameObject.GetComponent<Image>().color = Color.red;
                prevCircle = selectedCircle;
                prevNumber = selectedNumber;

                prevSelCir = true;
                prevSelNum = true;

                selectedCircle = null;
                selectedNumber = null;
                circleSelected = false;
                numberSelected = false;
            }
        }
    }

    public void GenerateFractions()
    {
        resetButton.interactable = false;

        foreach (GameObject fraction in fractions.ToArray())
        {
            fractions.Remove(fraction);
            Destroy(fraction);
        }

        for (int i = 0; i < 4; i++)
        {
            int denomenator = Random.Range(1, maxDenomenator);
            int numerator = Random.Range(1, maxNumerator > denomenator ? denomenator : maxNumerator);

            GameObject matchCircle = Instantiate(matchCirclePrefab);
            Image fillImage = matchCircle.transform.Find("Fraction Image").GetComponent<Image>();
            fillImage.fillAmount = (float)numerator / denomenator;

            CircleFraction circleFraction = matchCircle.GetComponent<CircleFraction>();
            circleFraction.numerator = numerator;
            circleFraction.denomenator = denomenator;

            GenerateLines(denomenator, matchCircle.GetComponentInChildren<Mask>().transform);

            GameObject matchNumber = Instantiate(matchNumberPrefab);
            matchNumber.GetComponentInChildren<Text>().text = "" + numerator + "/" + denomenator;

            NumberFraction numberFraction = matchNumber.GetComponent<NumberFraction>();
            numberFraction.numerator = numerator;
            numberFraction.denomenator = denomenator;

            fractions.Add(matchCircle);
            fractions.Add(matchNumber);

        }

        Shuffle(fractions, new System.Random());
        
        foreach (GameObject fraction in fractions.ToArray())
        {
            fraction.transform.SetParent(grid);
        }

        resetButton.interactable = true;
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

    public static void Shuffle<T>(IList<T> list, System.Random rnd)
    {
        for (var i = 0; i < list.Count; i++)
            Swap(list, i, rnd.Next(i, list.Count));
    }

    public static void Swap<T>(IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

}
