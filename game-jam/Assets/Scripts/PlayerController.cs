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
    private bool isAttacking = false;

    //audio

    public AudioSource JumpAudio;
    public AudioSource BoxHitAudio;
    public AudioSource FallOffMapAudio;
    public AudioSource SweepAudio;


    public AudioClip[] JumpArray;
    private int JumpClipIndex;

    public AudioClip[] BoxArray;
    private int BoxClipIndex;

    public AudioClip[] SweepArray;
    private int SweepClipIndex;


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
            anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
            return;
        }
        else
        {
            rb.MovePosition(transform.position + playerMoveInput * Time.deltaTime * movementMultiplier);
            anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
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
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpMultiplier);
            anim.SetBool("Jumping", true);
            PlayRandomJump();
        }
    }

    public void OnSweep(InputAction.CallbackContext context)
    {
        if(!isAttacking) StartCoroutine(Sweep());
    }
    IEnumerator Sweep()
    {
        isAttacking = true;
        anim.SetLayerWeight(anim.GetLayerIndex("Sweep Layer"), 1);
        anim.SetTrigger("Sweep");
        PlayRandomSweep();
        yield return new WaitForSeconds(0.8f);

        anim.SetLayerWeight(anim.GetLayerIndex("Sweep Layer"), 0);
        isAttacking = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            isGrounded = true;
            anim.SetBool("Jumping", false);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            PlayRandomBox();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            isGrounded = false;
        }
    }


    void PlayRandomJump()
    {
        JumpClipIndex = Random.Range(0, JumpArray.Length);
        JumpAudio.PlayOneShot(JumpArray[JumpClipIndex]);
    }

    void PlayRandomBox()
    {
        BoxClipIndex = Random.Range(0, BoxArray.Length);
        BoxHitAudio.PlayOneShot(BoxArray[BoxClipIndex]);
    }

    void PlayRandomSweep()
    {
        SweepClipIndex = Random.Range(0, SweepArray.Length);
        SweepAudio.PlayOneShot(SweepArray[SweepClipIndex]);
    }

}
