using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHp = 10;
    public int maxHp = 10;

    public bool dstryAtZero = true;

    public float invincTime = 0.3f;
    float invTimer = 0;

    float deathTime = 0.2f;
    bool dying = false;

    //Call this to change the health of the respective object
    public void HealthChange(int amount)
    {
        if (amount >= 0 || invTimer <= 0)
        {
            curHp += amount;
            if (amount < 0)
                invTimer = invincTime;
        }
        //if reduced to 0 and we need to destroy do that
        if (curHp <= 0 && dstryAtZero)
        {
            if (dying)
                StartCoroutine(TimedDestroy(deathTime));
        }
    }

    public IEnumerator TimedDestroy(float deathTime)
    {
        dying = true;
        yield return new WaitForSeconds(deathTime);
        Death d = GetComponent<Death>();
        if (d != null)
        {
            d.OnDeath.Invoke();
        }
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invTimer > 0)
        {
            invTimer -= Time.deltaTime;
        }
    }
}
