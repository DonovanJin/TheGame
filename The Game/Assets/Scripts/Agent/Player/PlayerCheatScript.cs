using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.Agent;
using Jincom.PickUps;

public class PlayerCheatScript : MonoBehaviour
{
    public WeaponData TestWeapon;

    public PlayerAgent playerAgent;

    public string PlayerHealth, PlayerArmour, WeaponAmmo, JumpHeight, SuitElement, AgentState;

    //public bool Testing;

    private void Start()
    {
        //if (Testing)
        //{
        //    playerAgent._playerData.AddWeapon(TestWeapon);
        //    playerAgent._playerData.CurrentWeapon = TestWeapon;
        //}        
    }

    void Update()
    {
        PlayerInput();
    }

    private void UpdatePlayerProperties()
    {
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAgent.playerData.SwitchToWeapon(TestWeapon);
        }
    }
}

