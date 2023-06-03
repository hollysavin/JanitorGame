using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb = null;

    Vector3 playerMoveInput = Vector3.zero;
    [SerializeField] float movementMultiplyer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + playerMoveInput * Time.deltaTime * movementMultiplyer);

        if (playerMoveInput == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(playerMoveInput);
        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
        rb.MoveRotation(targetRotation);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       playerMoveInput = new Vector3(context.ReadValue<Vector2>().x, 0.0f, context.ReadValue<Vector2>().y);
    }
}
