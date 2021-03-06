using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    public float deathTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CauseDeath());
    }

    public IEnumerator CauseDeath()
    {
        yield return new WaitForSeconds(deathTime);
        Death d = GetComponent<Death>();
        if (d != null)
        {
            d.OnDeath.Invoke();
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
