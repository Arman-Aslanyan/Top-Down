using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1000.0f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Fixed Update runs once per physics loop
    private void FixedUpdate()
    {
        //Grab the inputs and save
        float vertVel = Input.GetAxisRaw("Vertical");
        float horiVel = Input.GetAxisRaw("Horizontal");
        //Set force on the up vector
        rb.AddForce(transform.up * vertVel * speed * Time.fixedDeltaTime);
        rb.AddForce(transform.right * horiVel * speed * Time.fixedDeltaTime);
        //Optional: Rotate the player
        //rb.AddTorque(tramsform.right * horiVel * speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
