using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("The object the camera shall follow")]
    private Transform target;
    [Tooltip("How snappy the camera is from 0-1"), Range(0, 1)]
    public float lerpTVal = 0.5f;

    public float shakeTime = 0;
    public float shakeMagnitude = 0;

    public void TriggerShake(float time, float magnitude)
    {
        if (magnitude > shakeMagnitude)
        {
            shakeMagnitude = magnitude;
        }
        if (time > shakeTime)
        {
            shakeTime = time;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
            TriggerShake(1, 2);
        if (target != null)
        {
            //calculate the position to aim for
            Vector3 newPos = target.transform.position;
            newPos.z = transform.position.z;
            //lerp (lineraly interpolate) towards that point which smooths it
            transform.position = Vector3.Lerp(transform.position, newPos, lerpTVal);

            if (shakeTime > 0)
            {
                //Decrease shake timer
                shakeTime -= Time.fixedDeltaTime;
                Vector3 shakeDir = Random.insideUnitCircle;
                transform.position += shakeDir * shakeMagnitude;
            }
            else
            {
                shakeMagnitude = 0;
            }
        }
    }
}
