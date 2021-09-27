using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int pain = 4;
    public bool dstryOnCollider = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Health hp = collision.GetComponent<Health>();
        if (hp != null)
        {
            hp.HealthChange(-pain);
        }

        if (dstryOnCollider)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Health hp = collision.GetComponent<Health>();
        if (hp != null)
        {
            hp.HealthChange(-pain);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
