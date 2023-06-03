using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = null;

    private Animator anim;

    Vector3 playerMoveInput = Vector3.zero;
    [SerializeField] float movementMultiplier;
    [SerializeField] float jumpMultiplier;
    [SerializeField] float turnSpeed;

    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    private void ApplyMovement()
    {
        if (playerMoveInput == Vector3.zero)
        {
            anim.SetBool("Moving", false);
            return;
        }
        else
        {
            rb.MovePosition(transform.position + playerMoveInput * Time.deltaTime * movementMultiplier);
            anim.SetBool("Moving", true);
        }
    }

    private void ApplyRotation()
    {
        if (playerMoveInput == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(playerMoveInput);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        rb.MoveRotation(targetRotation);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMoveInput = new Vector3(context.ReadValue<Vector2>().x, 0.0f, context.ReadValue<Vector2>().y);

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            rb.AddForce(Vector2.up * jumpMultiplier);
        }
    }

    public void OnSweep(InputAction.CallbackContext context)
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            isGrounded = true;
            anim.SetBool("Jumping", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            isGrounded = false;
            anim.SetBool("Jumping", true);
        }
    }
}
