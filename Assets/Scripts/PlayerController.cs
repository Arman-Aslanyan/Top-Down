using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1000.0f;
    public float rotateSpeed = 10;
    private Rigidbody2D rb;

    private SpriteRenderer spR;
    public Sprite Up;
    public Sprite Down;

    CameraFollow CF;
    public float fireShakeTime = 0.1f;
    public float fireShakeMag = 0.2f;

    private ParticleSystem particleSys;

    //Laser Shoot variables
    public GameObject Laser;
    public GameObject largeBullet;
    public float cooldown = 0.2f;
    float timer = 0;
    public float LaserSpeed = 15;
    private float shotsFired = 0;
    public Vector3 offset1 = new Vector3(0.35f, 3.0f, 0);
    public Vector3 offset2 = new Vector3(-0.35f, 3.0f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spR = GetComponent<SpriteRenderer>();
        CF = FindObjectOfType<CameraFollow>();
        particleSys = GetComponentInChildren<ParticleSystem>();
        particleSys.Stop();
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
        //rb.AddTorque(-rotSpeed * Time.fixedDeltaTime)
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if max shots fired has been reached, if so then 
        if (shotsFired >= 10)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                shotsFired = 0;
                timer = 0;
                particleSys.Stop();
            }
        }
        //Checks to see if ship artillery has not reached safety heat limit
        else
        {
            particleSys.Play();
            //increase time based on time passed
            timer += Time.deltaTime;
            if (timer > cooldown && Input.GetAxisRaw("Jump") == 1)
            {
                //reset the timer
                timer = 0;
                Fire(offset1, false);
                Fire(offset2, false);
            }
            else if (timer > cooldown && Input.GetKeyDown(KeyCode.R))
            {
                Fire(Vector3.zero, true);
            }
        }

        //Change sprite depending on input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                spR.sprite = Down;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                spR.sprite = Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.S))
        {
            spR.sprite = Down;
        }
    }

    //spawns one object with an offset from the spawner
    void Fire(Vector3 offset, bool largeShot)
    {
        GameObject clone;
        Rigidbody2D cloneRb;
        Rigidbody2D cloneRbAgain;
        Vector3 largeBulOffset = new Vector3(0, -3.0f, 0);
        Vector3 largeBulOffset2 = new Vector3(0, 3.0f, 0);
        Vector3 spawnPos = transform.position + transform.rotation * largeBulOffset2;
        if (largeShot)
        {
            //Shoot very large bullet with drawbacks
            shotsFired += 10;

            //set the speed of the clone
            if (spR.sprite == Up)
            {
                Vector3 spawnPos2 = transform.position + transform.rotation * largeBulOffset;

                clone = Instantiate(largeBullet, spawnPos2, transform.rotation);
                cloneRb = clone.GetComponent<Rigidbody2D>();
                cloneRb.velocity = -transform.up * LaserSpeed;
            }
            else if (spR.sprite == Down)
            {
                clone = Instantiate(largeBullet, spawnPos, transform.rotation);
                cloneRb = clone.GetComponent<Rigidbody2D>();
                cloneRb.velocity = transform.up * LaserSpeed;
            }
            else
            {
                //Pain
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }

            cloneRb = largeBullet.GetComponent<Rigidbody2D>();
            cloneRb.velocity += rb.velocity;
            CF.TriggerShake(fireShakeTime * 2, fireShakeMag * 2);
        }
        else
        {
            shotsFired += 0.5f;
            //create the object with a position offset and affected by the rotation of the spawner
            if (spR.sprite == Up)
            {
                spawnPos = transform.rotation * offset;
            }
            else if (spR.sprite == Down)
            {
                spawnPos = transform.rotation * offset;
            }
            clone = Instantiate(Laser, spawnPos, transform.rotation);
            //set the speed of the clone
            cloneRbAgain = clone.GetComponent<Rigidbody2D>();
            if (spR.sprite == Up)
            {
                cloneRbAgain.velocity = -transform.up * LaserSpeed;
            }
            else if (spR.sprite == Down)
            {
                cloneRbAgain.velocity = transform.up * LaserSpeed;
            }
            cloneRbAgain.velocity += rb.velocity;
            CF.TriggerShake(fireShakeTime, fireShakeMag);
        }
    }
}
