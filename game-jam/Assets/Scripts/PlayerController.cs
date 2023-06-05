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
    public AudioSource playerAudioSource;
    public AudioClip[] JumpArray;
    public AudioClip[] BoxArray;
    public AudioClip[] SweepArray;
    private int clipIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        playerAudioSource = GetComponent<AudioSource>();
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
            PlayRandomSound(JumpArray, 0.4f);
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
        PlayRandomSound(SweepArray, 0.4f);

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
            PlayRandomSound(BoxArray, 1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Item")
        {
            isGrounded = false;
        }
    }

    void PlayRandomSound(AudioClip[] soundArray, float volume)
    {
        clipIndex = Random.Range(0, soundArray.Length);
        playerAudioSource.PlayOneShot(soundArray[clipIndex], volume);
    }
}
