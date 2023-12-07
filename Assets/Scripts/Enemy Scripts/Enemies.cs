using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enemy base stats -> arknights dmg formula
/*hp 100
 *def 20
 *speed 100
 *magic dmg ignores def but res reduces dmg in %
 *wip sistema de velocidad
 */

namespace EnemySystem
{
    [CreateAssetMenu(fileName = "enemy", menuName = "Create Enemy")]
    public class Enemies : ScriptableObject
    {
        public string Enemy_Name;
        public Sprite Enemy_Sprite;
        public List<Skills> Enemy_Skills = new List<Skills>();
        public int Enemy_Health;
        public int Enemy_Damage;
        public int Enemy_Defense;
        public EnemyClass Enemy_Class;

        
    }
    public enum EnemyClass
    {
        Knight,
        Mage,
        Thief,
        Berserker,
        Archer,
        Spirit,
        Elemental,
        Boss
    }
    [System.Serializable]
    public class Skills
    {
        public string Name;
        public int skill_multiplier;
        public SkillType skill_type;
        public TargetType Skill_target;
        public StatTarget skill_stat;
        public enum SkillType
        {
            Attack,
            Buff,
            Debuff,
            PreparationAttack,
            FinalAttack
        }
        public enum TargetType
        {
            Single,
            AoE
        }
        public enum StatTarget
        {
            Attack,
            Defense,
            MaxCost
        }
        public Skills(string _name, int _skillMultiplier, SkillType _skill, TargetType _target)
        {
            Name = _name;
            skill_multiplier = _skillMultiplier;
            skill_type = _skill;
            Skill_target = _target;
        }
    }
    //skills
}

