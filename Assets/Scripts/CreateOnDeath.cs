using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnDeath : MonoBehaviour
{
    [Tooltip("Drag the prefab you want made when this object destroys itself")]
    public GameObject prefabToMake;
    public Vector3 offset = Vector3.zero;
    public float randOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Vector3 spawnPos = transform.position + offset;
        Vector3 randVect3 = Random.insideUnitCircle * randOffset;
        Instantiate(prefabToMake, spawnPos + randVect3, transform.rotation);
    }
}
