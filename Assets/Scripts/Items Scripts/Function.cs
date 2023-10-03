using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Function", menuName = "Items/New Function")]
    public class Function : Items
    {
        public EffectType EffectType;
        public TargetType TargetType;
        public TargetStat StatMod;
        public bool magic, multihit, Percent;
        public int hits;
        public void Awake()
        {
            ItemType = ItemType.Function;
            StatMod = TargetStat.None;
            magic = false;
            multihit = false;
            hits = 0;
        }

        public enum TargetStat
        {
            None,
            Dmg,
            Def

        }
    }
}


