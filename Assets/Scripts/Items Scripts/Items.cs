using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Default Item")]
    public class Items : ScriptableObject
    {
        public Sprite ItemSprite;
        public string ItemName;
        [TextArea(10, 15)]
        public string ItemDescription;
        public int ItemRarity,ItemCost;
        public ItemType ItemType;
    }
    public enum ItemType
    {
        Equipment,
        Variable,
        Function,
        Consumible
    }
    public enum EffectType
    {
        Healing,
        Damage,
        Debuf,
        Buf
    }
    public enum TargetType
    {
        SingleTarget,
        AoE
    }
}