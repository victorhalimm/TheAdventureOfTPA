using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform camera;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float turnTime = 0.2f;

    float movementSpeed;

    [SerializeField] private Animator anim;

    bool atGround = false;

    float turnVelocity;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.enabled = false;
    }

    public void disableMovement()
    {
        this.enabled = false;
    }

    public void enableMovement()
    {
        this.enabled = true;
    }
    public void movePlayer()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(hAxis, 0f, vAxis);

        if (dir.magnitude == 0f) anim.SetFloat("Speed", 0f);



        if (dir.magnitude >= 0.1f)
        {

            if (Input.GetKey(KeyCode.LeftShift)) Run();
            else Walk();
            float pointedAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, pointedAngle, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, smoothAngle, 0f) * Vector3.forward;
            float currentYVelo = playerBody.velocity.y;
            playerBody.velocity = new Vector3(moveDir.x * movementSpeed * Time.deltaTime, currentYVelo, moveDir.z * movementSpeed * Time.deltaTime);


        }

        if (Input.GetKeyDown(KeyCode.Space) && atGround)
        {
            anim.SetTrigger("Jump");
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            atGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            atGround = true;
        }
    }

    public void Walk()
    {
        movementSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.fixedDeltaTime);
    }

    public void Run()
    {
        movementSpeed = runSpeed;
        anim.SetFloat("Speed", 1f, 0.1f, Time.fixedDeltaTime);
    }
}
