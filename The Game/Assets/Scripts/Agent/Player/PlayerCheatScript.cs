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

    private void Start()
    {
        playerAgent._playerData.AddWeapon(TestWeapon);
        playerAgent._playerData.CurrentWeapon = TestWeapon;
    }

    void Update()
    {
        //UpdatePlayerProperties();
    }

    private void UpdatePlayerProperties()
    {

    }
}

