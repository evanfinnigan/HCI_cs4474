﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingMode : MonoBehaviour {

    public Transform grid;

    public GameObject matchCirclePrefab;
    public GameObject matchNumberPrefab;

    public GameObject linePrefab;

    public int maxNumerator;
    public int maxDenomenator;

    public List<GameObject> fractions;

    public static bool generating = false;

    public Button resetButton;

    private void Start()
    {
        fractions = new List<GameObject>();
        GenerateFractions();
    }

    public void BtnReset()
    {
        GenerateFractions();
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

            GenerateLines(denomenator, matchCircle.GetComponentInChildren<Mask>().transform);

            GameObject matchNumber = Instantiate(matchNumberPrefab);
            matchNumber.GetComponentInChildren<Text>().text = "" + numerator + "/" + denomenator;

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
