using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [HideInInspector]
    public int points;

    public Text uiScore;

    void Start()
    {
        
    }

    void Update()
    {
        if(uiScore.text == "")
        {
            uiScore.text = 0.ToString();
        }

        uiScore.text = points.ToString();
    }
}
