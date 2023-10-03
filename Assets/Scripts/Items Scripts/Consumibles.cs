using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Consumible", menuName = "Items/New Consumible")]
    public class Consumibles : Items
    {
        public Sprite itemSprite;
        public int Uses;
        public EffectType cons_effect;
        public TargetType cons_target;
        public int cons_value;
        public bool Magic, BuffAtk, BuffDef, Multihit;
        public int Hits;
    }
}
