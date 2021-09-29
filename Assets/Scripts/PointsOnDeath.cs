using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnDeath : MonoBehaviour
{
    public int points = 5;

    private void OnDeath()
    {
        GameManager.score += points;
    }

    // Start is called before the first frame update
    void Start()
    {
        Death d = GetComponent<Death>();
        if (d != null)
        {
            d.OnDeath.AddListener(OnDeath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
