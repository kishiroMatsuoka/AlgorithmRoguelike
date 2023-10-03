using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Items/New Equipment")]
    public class Equipment : Items
    {
        //stats
        public int Attack;
        public int Defense;
        public int HP_Bonus;
        public void Awake()
        {
            ItemType = ItemType.Equipment;
        }
    }
}