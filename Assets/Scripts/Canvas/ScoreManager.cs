#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [System.NonSerialized]
    public static float Score;

    [System.NonSerialized]
    public static bool UpdateScore = false;

    [SerializeField]
    private Text scoreText;

    private const string uiText = "Score: ";

    private void Update()
    {
        //I wanted to add an Observer but it doesn't work quit well with subscribing and unsubscribing from every bullet.
        if (UpdateScore) 
        {
            scoreText.text = uiText + Score;
            UpdateScore = false;
        }
    }
}
