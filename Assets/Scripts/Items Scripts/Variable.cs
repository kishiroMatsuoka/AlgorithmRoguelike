using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Variable", menuName = "Items/New Variable")]
    public class Variable : Items
    {
        public Sprite icon;
        public int Dmg;
        public bool isPercentage;
        public bool IsMagic;
    }
}

