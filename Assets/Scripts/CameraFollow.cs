using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The object the camera shall follow")]
    private Transform target;
    [Tooltip("How snappy the camera is from 0-1"), Range(0, 1)]
    public float lerpTVal = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //transform.position = target.position + new Vector3(0, 0, -10);

            //calculate the position to aim for
            Vector3 newPos = target.transform.position;
            newPos.z = transform.position.z;
            //lerp (lineraly interpolate) towards that point which smooths it
            transform.position = Vector3.Lerp(transform.position, newPos, lerpTVal);
        }
    }
}
