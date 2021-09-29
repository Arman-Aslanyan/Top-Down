using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + GameManager.score;
    }
}
