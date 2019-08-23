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
        UpdatePlayerProperties();
        PlayerInput();
    }

    private void UpdatePlayerProperties()
    {
        PlayerHealth = playerAgent.playerData.CurrentHealth.ToString();
        PlayerArmour = playerAgent.playerData.CurrentArmour.ToString();       
        JumpHeight = playerAgent.playerData.JumpHeight.ToString();
        SuitElement = playerAgent.playerData.CurrentElement.ToString();
        AgentState = playerAgent.playerData._currentStateOfAgent.ToString();

        if (playerAgent.playerData.Ammo.ContainsKey(TestWeapon))
        {
            WeaponAmmo = playerAgent.playerData.Ammo[TestWeapon].ToString();
        }
        else
        {
            WeaponAmmo = "0";
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAgent.playerData.SwitchToWeapon(TestWeapon);
        }
    }
}

