using System;
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
        [System.Serializable]
        public enum TargetStat
        {
            None,
            Dmg,
            Def

        }
    }
}


