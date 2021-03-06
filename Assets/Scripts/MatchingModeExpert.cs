﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingModeExpert : MonoBehaviour
{

    public Transform grid;

    public GameObject matchNumberPrefab;
    public AudioSource audioCorrect;
    public AudioSource audioIncorrect;

    public int maxNumerator;
    public int maxDenomenator;

    public int maxMultiplier;

    public List<GameObject> fractions;

    public static bool generating = false;

    public Button resetButton;

    NumberFraction selectedNumber1;
    NumberFraction selectedNumber2;
    bool number1Selected;
    bool number2Selected;

    NumberFraction prevNumber1;
    NumberFraction prevNumber2;
    bool prevSelected = false;

    public Text levelText;
    int level;

    private void Start()
    {
        level = 1;
        levelText.text = "Level " + level;

        number1Selected = false;
        number2Selected = false;
        fractions = new List<GameObject>();
        GenerateFractions();
    }

    public void BtnReset()
    {
        level = 1;
        levelText.text = "Level " + level;
        GenerateFractions();
    }

    public void BtnSelectNumber(NumberFraction number)
    {
        if (number1Selected && selectedNumber1 == number)
        {
            if (prevSelected)
            {
                prevNumber1.gameObject.GetComponent<Image>().color = Color.white;
                prevNumber2.gameObject.GetComponent<Image>().color = Color.white;
            }
            selectedNumber1.gameObject.GetComponent<Image>().color = Color.white;
            number1Selected = false;
            selectedNumber1 = null;
        }
        else if (number2Selected && selectedNumber2 == number)
        {
            if (prevSelected)
            {
                prevNumber1.gameObject.GetComponent<Image>().color = Color.white;
                prevNumber2.gameObject.GetComponent<Image>().color = Color.white;
            }
            selectedNumber2.gameObject.GetComponent<Image>().color = Color.white;
            number2Selected = false;
            selectedNumber2 = null;
        }
        else if (!number1Selected)
        {
            if (prevSelected)
            {
                prevNumber1.gameObject.GetComponent<Image>().color = Color.white;
                prevNumber2.gameObject.GetComponent<Image>().color = Color.white;
            }
            number1Selected = true;
            selectedNumber1 = number;
            selectedNumber1.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else if (!number2Selected)
        {
            if (prevSelected)
            {
                prevNumber1.gameObject.GetComponent<Image>().color = Color.white;
                prevNumber2.gameObject.GetComponent<Image>().color = Color.white;
            }
            number2Selected = true;
            selectedNumber2 = number;
            selectedNumber2.gameObject.GetComponent<Image>().color = Color.yellow;
        }

        TestSelected();
    }

    public void TestSelected()
    {
        if (number1Selected && number2Selected)
        {
            if (selectedNumber1.numerator == selectedNumber2.numerator && selectedNumber1.denomenator == selectedNumber2.denomenator
                ||
                Mathf.Abs(((float)selectedNumber1.numerator/selectedNumber1.denomenator) - ((float)selectedNumber2.numerator/selectedNumber2.denomenator)) < 0.0001)
            {
                Debug.Log("Correct!");
                audioCorrect.Play();
                selectedNumber1.gameObject.GetComponent<Image>().color = Color.green;
                selectedNumber2.gameObject.GetComponent<Image>().color = Color.green;
                selectedNumber1.gameObject.transform.Find("Correct").gameObject.SetActive(true);
                selectedNumber2.gameObject.transform.Find("Correct").gameObject.SetActive(true);
                selectedNumber1.gameObject.GetComponent<Button>().interactable = false;
                selectedNumber2.gameObject.GetComponent<Button>().interactable = false;
                selectedNumber1 = null;
                selectedNumber2 = null;
                number1Selected = false;
                number2Selected = false;
                prevSelected = false;

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
                prevSelected = true;

                prevNumber1 = selectedNumber1;
                prevNumber2 = selectedNumber2;
                
                selectedNumber1.gameObject.GetComponent<Image>().color = Color.red;
                selectedNumber2.gameObject.GetComponent<Image>().color = Color.red;
                selectedNumber1 = null;
                selectedNumber2 = null;
                number1Selected = false;
                number2Selected = false;
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
            int numerator = Random.Range(1, maxNumerator);

            GameObject matchNumber1 = Instantiate(matchNumberPrefab);
            int multiplier = Random.Range(2, maxMultiplier * level);
            matchNumber1.GetComponentInChildren<Text>().text = "" + multiplier * numerator + "/" + multiplier * denomenator;

            NumberFraction numberFraction1 = matchNumber1.GetComponent<NumberFraction>();
            numberFraction1.numerator = numerator;
            numberFraction1.denomenator = denomenator;

            GameObject matchNumber2 = Instantiate(matchNumberPrefab);
            matchNumber2.GetComponentInChildren<Text>().text = "" + numerator + "/" + denomenator;

            NumberFraction numberFraction2 = matchNumber2.GetComponent<NumberFraction>();
            numberFraction2.numerator = numerator;
            numberFraction2.denomenator = denomenator;

            fractions.Add(matchNumber1);
            fractions.Add(matchNumber2);
        }

        Shuffle(fractions, new System.Random());

        foreach (GameObject fraction in fractions.ToArray())
        {
            fraction.transform.SetParent(grid);
        }

        resetButton.interactable = true;
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

