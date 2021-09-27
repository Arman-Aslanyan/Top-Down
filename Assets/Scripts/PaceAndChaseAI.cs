using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaceAndChaseAI : MonoBehaviour
{
    [Tooltip("Insert the list of points you want the AI to pace through.")]
    public Vector3[] points = { new Vector3(0, 0, 0), new Vector3(5, 0, 0) };

    public int curPoint = 0;

    public float paceSpeed = 2;
    public float closeEnough = 0.1f;
    public float chaseDistance = 4;
    public float chaseSpeed = 5;

    [Tooltip("The object that AI shall chase/run away from")]
    public GameObject target;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        //Make sure target exists
        if (target != null)
        {
            //grab the Vector 2 to the player
            Vector2 dir = target.transform.position - transform.position;
            //check if within chase distance or not
            if (dir.sqrMagnitude <= chaseDistance * chaseDistance)
            {
                Chase(dir);
            }
            else
            {
                Pace();
            }
        }
        else
        {
            //If it doesn't exist then just pace
            Pace();
        }
    }

    void Pace()
    {
        //check if near current target point, if so then move to next
        Vector3 dir = points[curPoint] - transform.position;
        float distSquared = dir.sqrMagnitude;
        if (distSquared < closeEnough)
        {
            ++curPoint;
            if (curPoint >= points.Length)
                curPoint = 0;
            dir = points[curPoint] - transform.position;
        }

        //set movement towards current target
        //normalize to make length 1
        dir = dir.normalized;
        Vector2 acceleration = dir * paceSpeed * Time.deltaTime;
        rb.velocity += acceleration;
    }

    void Chase(Vector2 dir)
    {
        rb.velocity += dir.normalized * chaseSpeed * Time.fixedDeltaTime;
    }
}
