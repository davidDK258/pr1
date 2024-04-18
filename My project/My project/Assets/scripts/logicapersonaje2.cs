using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicapersonaje2 : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    private float x, y;
    private bool isGrounded = true; // Grounded boolean
    private bool isRunning = false; // Running boolean
    private bool isJumping = false; // Jumping boolean
    public float raycastDistance = 1.0f; // Distancia del Raycast hacia abajo

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);

        // Check if running
        isRunning = Input.GetKey(KeyCode.LeftShift) && y > 0; // Assuming LeftShift is the run key

        if (isRunning)
        {
            y *= 2; // Double the speed when running
        }

        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

        anim.SetFloat("velx", x);
        anim.SetFloat("vely", y);

        // Check if the player is grounded using Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        anim.SetBool("Grounded", isGrounded);

        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Check if there is an obstacle above before jumping
            if (!Physics.Raycast(transform.position, transform.up, raycastDistance))
            {
                isJumping = true;
                anim.SetTrigger("JumpUp");
            }
        }
        else
        {
            isJumping = false;
        }

        anim.SetBool("Salto", isJumping); // Salto is jump in Spanish
        anim.SetBool("Run", isRunning); // Set running animation parameter
    }
}
