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
        if (UpdateScore) 
        {
            scoreText.text = uiText + Score;
            UpdateScore = false;
        }
    }
}
