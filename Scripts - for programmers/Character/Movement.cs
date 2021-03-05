using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] float moveSpeed;


    //Jump
    [SerializeField] float jumpHeight;
    [SerializeField] float doubleJumpHeight;
    [SerializeField] float jumpWaitTime;
    [SerializeField] float jumpCooldownTime;

    //Jump Sounds
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip doubleJumpSFX;
    [SerializeField] AudioClip landSFX;


    //Dash
    [SerializeField] float dashOnCooldownTime;
    [SerializeField] bool dashAbility;
    [SerializeField] bool canDoubleJump;
    [SerializeField] GameObject energyParticles;

    //Dash Sound
    [SerializeField] AudioClip dashSFX;

    public bool isGrounded = true;
    public bool dashing;
    private bool dashOnCooldown;
    private bool jumpCooldown;
    private bool moving;
    private int jumpCount = 0;
    private float startingMoveSpeed;


    //Vector3 Variables
    Vector2 input;
    Vector3 moveVelocity;
    Vector3 moveInput;



    //Cached References
    Rigidbody rigidBody;
    Animator animator;
    Camera mainCamera;
    Ray lookRay;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        startingMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Run();

        StartCoroutine(Jump());

        //Check if dash ability is enabled, 'E' key is pressed, and dash is not on cooldown
        DashCheck();

        DoubleJump();
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && moving == true)
        {
            if(moveSpeed < startingMoveSpeed * 2)
            {
            moveSpeed *= 2;
            }
        }
        else if(dashing == false)
        {
            moveSpeed = startingMoveSpeed;
        }
    }

    private void DoubleJump()
    {
        if (canDoubleJump && jumpCount == 1 && Input.GetButtonDown("Jump") && jumpCooldown == false)
        {
            AudioSource.PlayClipAtPoint(doubleJumpSFX, transform.position);
            rigidBody.AddForce(new Vector3(0f, doubleJumpHeight, 0f), ForceMode.Impulse);
            rigidBody.AddForce(Vector3.forward * doubleJumpHeight/3, ForceMode.Impulse);
            jumpCount = 2;
        }
    }

    private void DashCheck()
    {
        if (dashAbility && Input.GetKeyDown(KeyCode.E) && !dashOnCooldown & isGrounded == false)
        {
            StartCoroutine(Dash());
        }
    }

    private void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(input.x, 0, input.y).normalized;
        moveVelocity = transform.forward * moveSpeed * moveInput.sqrMagnitude;

        Vector3 CameraForward = mainCamera.transform.forward;
        CameraForward.y = 0;

        Quaternion CameraRelativeRotation = Quaternion.FromToRotation(Vector3.forward, CameraForward);

        Vector3 looktoward = CameraRelativeRotation * moveInput;

        if (moveInput.sqrMagnitude > 0)
        {
            lookRay = new Ray(transform.position, looktoward);
            Vector3 dir = lookRay.GetPoint(1);
            dir.y = transform.position.y;
            transform.LookAt(dir);
            moving = true;
        }
        else
        {
            moving = false;
        }


        //Blend Tree animation for moving
        animator.SetFloat("Speed", rigidBody.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        moveVelocity.y = rigidBody.velocity.y;
        rigidBody.velocity = moveVelocity;

    }

    IEnumerator Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
            rigidBody.AddForce(new Vector3(0f, jumpHeight, 0f), ForceMode.Impulse);
            animator.SetBool("jump", true);
            isGrounded = false;
            jumpCount = 1;
            jumpCooldown = true;
            yield return new WaitForSeconds(jumpCooldownTime);
            jumpCooldown = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        jumpCount = 0;
        if (animator.GetBool("jump") == true)
        {
            animator.SetBool("jump", false);
            AudioSource.PlayClipAtPoint(landSFX, transform.position);
        }

    }

    IEnumerator Dash()
    {
        dashOnCooldown = true;

        float cameraTurnSpeedTemp = transform.parent.GetComponentInChildren<UnityStandardAssets.Cameras.FreeLookCam>().m_TurnSpeed;

        //Start dash
        moveSpeed *= 3;
        transform.parent.GetComponentInChildren<UnityStandardAssets.Cameras.FreeLookCam>().m_TurnSpeed = 0.5f;
        GameObject dashParticles = Instantiate(energyParticles, transform.position, Quaternion.identity);
        dashing = true;
        AudioSource.PlayClipAtPoint(dashSFX, transform.position);


        //Continue after 1 second
        yield return new WaitForSeconds(1);

        //Stop dash
        moveSpeed /= 3;
        transform.parent.GetComponentInChildren<UnityStandardAssets.Cameras.FreeLookCam>().m_TurnSpeed = cameraTurnSpeedTemp;
        dashing = false;
        Destroy(dashParticles);

        //Continue after a further 3 seconds
        yield return new WaitForSeconds(dashOnCooldownTime);

        dashOnCooldown = false;
    }
}