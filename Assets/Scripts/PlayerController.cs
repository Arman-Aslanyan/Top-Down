using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1000.0f;
    public float rotateSpeed = 10;
    private Rigidbody2D rb;

    //Laser Shoot variables
    public GameObject Laser;
    public float cooldown = 0.2f;
    float timer = 0;
    public float LaserSpeed = 15;
    public Vector3 offset1 = new Vector3(0.35f, 3.0f, 0);
    public Vector3 offset2 = new Vector3(-0.35f, 3.0f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Fixed Update runs once per physics loop
    private void FixedUpdate()
    {
        //grab input from the user
        float xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        float ySpeed = Input.GetAxisRaw("Vertical") * speed;
        //float rotSpeed = Input.GetAxisRaw("Horizontal") * rotateSpeed;

        //Add forces and torque
        rb.AddForce(transform.right * xSpeed * Time.fixedDeltaTime);
        rb.AddForce(transform.up * ySpeed * Time.fixedDeltaTime);
        //rb.AddTorque(-rotSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //increase time based on time passed
        timer += Time.deltaTime;
        if (timer > cooldown && Input.GetAxisRaw("Jump") == 1)
        {
            //reset the timer
            timer = 0;
            Fire(offset1);
            Fire(offset2);
        }
    }

    //spawns one object with an offset from the spawner
    void Fire(Vector3 offset)
    {
        //create the object with a position offset and affected by the rotation of the spawner
        Vector3 spawnPos = transform.position + transform.rotation * offset;
        GameObject clone = Instantiate(Laser, spawnPos, transform.rotation);
        //set the speed of the clone
        Rigidbody2D cloneRb = clone.GetComponent<Rigidbody2D>();
        cloneRb.velocity = -transform.up * LaserSpeed;
    }
}
