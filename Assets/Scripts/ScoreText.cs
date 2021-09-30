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
        score = gameObject.GetComponent<TMP_Text>();
        ChangeText();
        GameManager.OnScoreChange.AddListener(ChangeText);
    }

    public void ChangeText()
    {
        score.text = "Score: " + GameManager.score;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
