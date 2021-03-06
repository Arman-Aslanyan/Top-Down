using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnDeath : MonoBehaviour
{
    [Tooltip("Drag the prefab you want made when this object destroys itself")]
    public GameObject prefabToMake;
    public Vector3 offset = Vector3.zero;
    public float randOffset = 0;

    private void OnDeath()
    {
        Vector3 spawnPos = Random.insideUnitCircle * randOffset;
        spawnPos += transform.position + offset;
        Instantiate(prefabToMake, spawnPos, transform.rotation);
    }

    void Start()
    {
        Death d = GetComponent<Death>();
        if (d != null)
        {
            d.OnDeath.AddListener(OnDeath);
        }
    }
}
