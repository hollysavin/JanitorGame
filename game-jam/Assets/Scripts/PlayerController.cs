using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = null;

    private Animator anim;

    Vector3 playerMoveInput = Vector3.zero;
    
    [SerializeField] private float movementMultiplier;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform holdSpot;

    private bool isGrounded = false;
    private bool isHolding = false;


    //audio
    public AudioSource playerAudioSource;
    public AudioClip[] JumpArray;
    public AudioClip[] BoxArray;
    public AudioClip[] SweepArray;
    private int clipIndex;
    private string playerName = null;

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

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!isHolding) PickUpItem();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (isHolding) ThrowItem();
    }

    private void PickUpItem()
    {
        int layerMask = LayerMask.GetMask("Items");
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.6f, layerMask))
        {
            if (gameObject.transform.parent == null)
            {
                GameObject currentItem = hit.transform.gameObject;
                Destroy(currentItem.GetComponent<Rigidbody>());
                currentItem.transform.position = holdSpot.transform.position;
                currentItem.transform.parent = holdSpot;
                Item itemScript = currentItem.GetComponent<Item>();
                if (itemScript != null)
                {
                    itemScript.SetItemOwner(playerName);
                }
                isHolding = true;
            }
        }
    }

    private void ThrowItem()
    {
        GameObject gameObject = holdSpot.GetChild(0).gameObject;
        gameObject.gameObject.transform.parent = null;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 450);
        isHolding = false;
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

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }
}
