using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnDeath : MonoBehaviour
{
    public int points = 5;

    // Start is called before the first frame update
    void Start()
    {
        Death d = GetComponent<Death>();
        if (d != null)
            d.OnDeath.AddListener(AddPoints);
    }

    public void AddPoints()
    {
        GameManager.score += points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
