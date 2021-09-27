using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int curHp = 10;
    public int maxHp = 10;

    public bool dstryAtZero = true;
    private SpriteRenderer sprRend;
    private bool deathAnim = true;

    //Call this to change the health of the respective object
    public void HealthChange(int changeHp)
    {
        curHp += changeHp;
        if (curHp <= 0)
        {
            //optional: Negats negative hp
            curHp = 0;
            if (dstryAtZero)
            {
                //StartCoroutine(TimedDestroy(0.2f, 255));
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator TimedDestroy(float deathTime, int trans)
    {
        while (deathAnim)
        {
            yield return new WaitForSeconds(deathTime);
            sprRend.color = new Color(1,1,1,trans);
            trans--;

            if (trans == 0)
            {
                deathAnim = false;
            }
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
