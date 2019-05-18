using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.PickUps;

namespace Jincom.Agent
{
    public class ExampleScript : MonoBehaviour
    {
        public WeaponData Data;

        public void FireGun()
        {
            Debug.Log(Data.Damage);
        }
    }
}