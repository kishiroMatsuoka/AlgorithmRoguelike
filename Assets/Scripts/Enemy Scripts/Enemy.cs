using System.Collections;
using System.Collections.Generic;
using EnemySystem;
using UnityEngine;

//interactions script
public class Enemy : MonoBehaviour
{
    [SerializeField] Enemies enemydata;
    public int _enemylvl, _enemyhp, _enemyspeed, _enemydmg, _enemydef;
    public bool Recursion = false;
    //local calculations
    Combat_controller cc;
    int maxhp, dmg_effect = 0, def_effect = 0, BossBonusDmg = 0;
    public bool IsDead = false;
    Skills last_used = null;
    void Start()
    {
        cc = FindObjectOfType<Combat_controller>();
        //_enemylvl = Random.Range(1, GameObject.Find("EventSystem").GetComponent<SceneControl>().MaxLvlZone+1);
        _enemyhp = enemydata.Enemy_Health;
        _enemydmg = enemydata.Enemy_Damage;
        _enemydef = enemydata.Enemy_Defense;
        ScaleStats();
        maxhp = _enemyhp;
    }
    private void Update()
    {
        if(_enemyhp <= 0) { IsDead = true; }
    }
    public Sprite GetEnemySprite()
    {
        return enemydata.Enemy_Sprite;
    }
    void ScaleStats()
    {
        float scale = _enemylvl * 0.1f;
        if(_enemylvl > 1)
        {
            _enemyhp = (int)(_enemyhp * scale);
            _enemydmg = (int)(_enemydmg* scale);
            _enemydef = (int)(_enemydef * scale);
        }
    }
    float ClassRes()
    {
        float res = 0f;
        switch (enemydata.Enemy_Class)
        {
            case EnemyClass.Mage:
                res = 25.0f;
                break;
            case EnemyClass.Boss:
                res = 10.0f;
                break;
            case EnemyClass.Elemental:
                res = 99.0f;
                break;
        }
        return res;
    }
    public void ChangeHp(int effect, bool magic)
    {
        if(magic)
        {
            float res = ClassRes();
            if(res > 0)
            {
                float reduction = effect - (effect * res / 100);
                effect = (int)System.Math.Round(reduction);
            }
            print("Enemy Receive mag dmg " + effect);
            _enemyhp -= effect;
        }
        else
        {
            print("Enemy Receive dmg, " +
                "Dmg calculated: " + (effect - (_enemydef + def_effect)));
            _enemyhp -= (effect-(_enemydef+def_effect));
        }
        //check if dead
        Debug.Log("Enemy hp =" + _enemyhp);
        if(_enemyhp <= 0)
        {
            IsDead = true;
            Debug.Log("Enemy dead? =" + IsDead);
            if (enemydata.Enemy_Class == EnemyClass.Boss)
            {
                FindObjectOfType<SceneControl>().BossDead = true;
            }
        }
    }
    void Heal(int value)
    {
        _enemyhp += value;
        if(_enemyhp > maxhp){_enemyhp = maxhp;}
    }
    int CalculateDmg(Skills s)
    {
        
        int dmg = (int)System.Math.Round((_enemydmg+dmg_effect) * s.skill_multiplier / 100f);
        if (enemydata.Enemy_Class == EnemyClass.Boss)
        {
            dmg += BossBonusDmg;
        }
        return dmg;
    }
    public void AttackEffect(int percentage, bool buff)
    {
        if(buff)
        {
            dmg_effect += (int)System.Math.Round((_enemydmg * percentage / 100f));
        }
        else
        {
            dmg_effect -= (int)System.Math.Round((_enemydmg * percentage / 100f));
        }
    }
    public void DefEffect(int percentage, bool buff)
    {
        if (buff){def_effect += (int)System.Math.Round((_enemydef * percentage / 100f));}
        else{def_effect -= (int)System.Math.Round((_enemydef * percentage / 100f));}
    }
    public void UseSkill(Player_Controller pc)
    {
        if (last_used != null)
        {
            print("Enemigo Usa Ataque Potente");
            List<Skills> temp =  enemydata.Enemy_Skills.FindAll(s => s.skill_type == Skills.SkillType.FinalAttack);
            Skills skill = temp[Random.Range(0, temp.Count)];
            if (skill.Skill_target == Skills.TargetType.Single)
            {
                pc.PartyRecieveDmg(CalculateDmg(skill), false);
                dmg_effect = 0;
            }
            else
            {
                pc.PartyRecieveDmg(CalculateDmg(skill), true);
            }
            SpawnText(skill.Name);
            last_used = null;
        }
        else
        {
            Skills skill = enemydata.Enemy_Skills[Random.Range(0, enemydata.Enemy_Skills.Count)];
            switch(skill.skill_type)
            {
                case Skills.SkillType.Attack:
                    print("Enemigo usa Basic Attack ");
                    if(skill.Skill_target == Skills.TargetType.Single)
                    {
                        pc.PartyRecieveDmg(CalculateDmg(skill),false);
                        dmg_effect = 0;
                    }
                    else//AoE
                    {
                        pc.PartyRecieveDmg(CalculateDmg(skill), true);
                    }
                    SpawnText(skill.Name);
                    break;
                case Skills.SkillType.Buff:
                    print("Enemigo usa Buff Skill");
                    if (skill.Skill_target == Skills.TargetType.Single)
                    {
                        switch (skill.skill_stat)
                        {
                            case Skills.StatTarget.Attack:
                                AttackEffect(skill.skill_multiplier, true);
                                break;
                            case Skills.StatTarget.Defense:
                                DefEffect(skill.skill_multiplier, true);
                                break;
                        }
                    }
                    else
                    {
                        foreach(Enemy e in cc.n_enemy.FindAll(x =>!x.IsDead))
                        {
                            if (skill.skill_stat == Skills.StatTarget.Attack)
                            {
                                AttackEffect(skill.skill_multiplier, true);
                            }
                            else
                            {
                                DefEffect(skill.skill_multiplier, true);
                            }
                        }
                    }
                    SpawnText(skill.Name);
                    break;
                case Skills.SkillType.Debuff:
                    print("Enemigo usa debuf");
                    if (skill.Skill_target == Skills.TargetType.Single)
                    {
                        switch (skill.skill_stat)
                        {
                            case Skills.StatTarget.Attack:
                                cc.StatModifier(skill.skill_multiplier, false, 0);
                                break;
                            case Skills.StatTarget.Defense:
                                cc.StatModifier(skill.skill_multiplier, false, 1);
                                break;
                            case Skills.StatTarget.MaxCost:
                                cc.StatModifier(skill.skill_multiplier, false, 2);
                                break;
                        }
                        SpawnText(skill.Name);
                    }
                    else
                    {
                        switch (skill.skill_stat)
                        {
                            case Skills.StatTarget.Attack:
                                cc.StatModifier(skill.skill_multiplier, false, 0);
                                break;
                            case Skills.StatTarget.Defense:
                                cc.StatModifier(skill.skill_multiplier, false, 1);
                                break;
                            case Skills.StatTarget.MaxCost:
                                cc.StatModifier(skill.skill_multiplier, false, 2);
                                break;
                        }
                        pc.PartyEffect(skill.skill_stat,skill.skill_multiplier, false);
                        SpawnText(skill.Name);
                    }
                    break;
                case Skills.SkillType.PreparationAttack:
                    Debug.Log("Enemigo Prepara un Ataque");
                    SpawnText(skill.Name);
                    last_used = skill;
                    break;
                case Skills.SkillType.FinalAttack:
                    UseSkill(pc);
                    break;
            }
        }
        if (Recursion) { BossBonusDmg += 2; }
    }
    void SpawnText(string skillname)
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += 1f; 
        var ft1 = Instantiate(cc.FloatingText, pos, Quaternion.identity);
        ft1.GetComponent<AutoDeleteText>()._text.text = skillname;
    }
}

