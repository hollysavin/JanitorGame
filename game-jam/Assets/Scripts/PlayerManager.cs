using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private int currentCount = 0;
    [SerializeField]
    private Material[] playerOutfits, playerOutlines;
    [SerializeField]
    private Transform[] playerPositions;
    [SerializeField]
    private Color[] arrowColors;
    [SerializeField]
    private String[] names;


    private void OnEnable()
    {
        PlayerInputManager.instance.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        PlayerInputManager.instance.onPlayerJoined -= AddPlayer;
    }


    private void AddPlayer(UnityEngine.InputSystem.PlayerInput newPlayer)
    {
        Renderer[] renderers = newPlayer.GetComponentsInChildren<Renderer>();
        renderers[0].material = playerOutfits[currentCount];
        renderers[1].material = playerOutlines[currentCount];
        newPlayer.GetComponentInChildren<Image>().color = arrowColors[currentCount];
        newPlayer.GetComponentInChildren<TextMeshProUGUI>().text = names[currentCount];
        newPlayer.GetComponentInChildren<Transform>().position = playerPositions[currentCount].position;

        PlayerController playerCon = newPlayer.GetComponentInChildren<PlayerController>();
        if (playerCon != null)
        {
            playerCon.SetPlayerName(names[currentCount]);
        }

        currentCount++;
    }

}
