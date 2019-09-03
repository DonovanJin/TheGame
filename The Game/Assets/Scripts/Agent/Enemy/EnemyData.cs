using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.PickUps;

namespace Jincom.Agent
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/Agent")]
    public class EnemyData : ScriptableObject
    {
        public int MaxHealth = 100;
        public int MaxArmour = 100;
        public float JumpHeight = 650f;
        public GameConstants.Elements ArmourElement;
        public WeaponData EnemyWeapon;
    }
}