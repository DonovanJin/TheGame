using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.PickUps
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "PickUp/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public string WeaponName = null;
        public float Range = 0f;
        public int Damage = 0;
        public GameConstants.Elements Element = GameConstants.Elements.Normal;
    }
}