using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Code", menuName = "Items/New code")]
    public class Code : Items
    {
        public int Cost;
        public Function[] Merged_Functions;
        public void Awake()
        {
            ItemType = ItemType.Function;
        }
    }
}