using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerConnectPrompt : MonoBehaviour
{
    public GameObject dialog;

    void Start()
    {
        if (GameManager.instance != null)
        {
            if (!GameManager.instance.IsNetworked()) { 
                TextMeshProUGUI tmp = dialog.GetComponent<TextMeshProUGUI>();
                tmp.text = "Press any button on Keyboard or Controller to connect to players. \nPlease make sure to press them in the order: Player 1 then Player 2";
            }
        }   
    }
}
