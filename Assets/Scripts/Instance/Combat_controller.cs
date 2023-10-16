using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using TMPro;


public class Combat_controller : MonoBehaviour 
{
    public GameObject fun_prefab, var_prefab, cons_prefab, enemy_parent
        , itemdesc, CombatResultScreen;
    public List<Actions> ActionsInBoard = new List<Actions>();
    public int e_dmg = 0, e_def = 0, turn_counter, Max_cost, Current_cost, e_cost;
    public List<Enemy> n_enemy;
    public Transform Board, fun_inv, var_inv, con_inv;
    [SerializeField] TextMeshProUGUI turn_text, CostCounter, Cost_maxtxt,extraCost;
    Player_Controller pc;
    public bool Dragging = false;
    public int combat_score,c_dmg;
    bool StopCombat = false;
    void OnEnable()
    {
        //Combat Variables Settings//
        pc = GameObject.Find("Player").GetComponent<Player_Controller>();
        combat_score = 1000;c_dmg = 0;
        Max_cost = pc._maxcost;
        Current_cost = 0;e_cost = 0;turn_counter = 1;
        Cost_maxtxt.text = "Max:" + pc._maxcost;turn_text.text = "1";
        //Enemy Spawning//
        FillInventory();
        SpawnEnemies();
    }
    public void DmgScore(int dmg)
    {
        c_dmg += dmg;
        if(c_dmg >= 50) { combat_score -= 100;c_dmg = 0; }
    }
    public void AdvanceTurn(){
        //Update turn counter
        turn_counter++;
        turn_text.text = turn_counter.ToString();
        //play board
        foreach (Actions action in ActionsInBoard) {
            if (!StopCombat)
            {
                CodeToExecute(action);
            }
        }
        StopCombat =  CheckDead();
        if (!CombatResultScreen.activeSelf  && !StopCombat)
        {
            //enemy turn
            foreach (Enemy x in n_enemy) {
                if (!x.IsDead) {
                    print("Enemigo Elige Skill");
                    x.UseSkill(pc); }
            }
        }
        //remove buffs
        e_dmg = 0;
        e_def = 0;
        e_cost = 0;
        UpdateCostMod();
        //returns functions to inventory
        Board.GetComponent<Attach_Zone>().TurnFinished();
    }
    public bool CheckCost(int card_cost)
    {
        int temp = Current_cost + card_cost;
        if(temp <= (Max_cost+e_cost))
        {
            Current_cost = temp;
            CostCounter.text = "Cost:"+Current_cost;
            print("Opcion posible: result" + temp);
            return true;
        }
        else{print("Opcion no posible: result" + temp);return false;}
    }
    void UpdateCostMod()
    {
        if (e_cost > 0)
        {
            extraCost.transform.parent.gameObject.SetActive(true);
            extraCost.text = "+" + e_cost + "=" + (Max_cost + e_cost);
        }
        else if(e_cost < 0)
        {
            extraCost.transform.parent.gameObject.SetActive(true);
            extraCost.text = "-" + e_cost + "=" + (Max_cost + e_cost);
        }
        else
        {
            extraCost.transform.parent.gameObject.SetActive(false);
        }
    }
    bool CheckDead()
    {
        n_enemy.RemoveAll(x => x.IsDead == true);
        if (n_enemy.Count == 0)
        {
            CombatResultScreen.SetActive(true);
            return true;
        }
        return false;
    }
    public void CodeToExecute(Actions code)
    {
        if(code.con_reference == null)
        {
            Function fun = code.fun_reference;
            Variable var = code.var_reference;
            EffectType effect = fun.EffectType;
            TargetType target = fun.TargetType;
            switch (effect)
            {
                case EffectType.Damage:
                    int dmg = var.Dmg;
                    dmg += (int)System.Math.Round(dmg * e_dmg / 100f);
                    TargetType targets = fun.TargetType;
                    if (targets == TargetType.SingleTarget)
                    {
                        if (fun.multihit)
                        {
                            for (int i = 0; i < fun.hits; i++){n_enemy[0].ChangeHp(dmg, fun.magic);}
                            if (n_enemy[0].IsDead){n_enemy.Remove(n_enemy[0]);}
                        }
                        else
                        {
                            n_enemy[0].ChangeHp(dmg, fun.magic);
                            CheckDead();
                        }
                    }
                    else//AOE
                    {
                        foreach (Enemy t in n_enemy)
                        {
                            if (fun.multihit){for (int x = 0; x < fun.hits; x++){t.ChangeHp(dmg, fun.magic);}}
                            else{t.ChangeHp(dmg, fun.magic);}
                        }
                        CheckDead();
                    }
                    break;
                case EffectType.Healing:

                    break;
                case EffectType.Debuf:
                    if (target == TargetType.SingleTarget){
                        if (fun.StatMod == Function.TargetStat.Dmg){n_enemy[0].AttackEffect(var.Dmg, false);}
                        else{n_enemy[0].DefEffect(var.Dmg, false);}
                    }
                    else
                    {
                        foreach (Enemy t in n_enemy)
                        {
                            if (fun.StatMod == Function.TargetStat.Dmg){t.AttackEffect(var.Dmg, false);}
                            else{t.DefEffect(var.Dmg, false);}
                        }
                    }
                    break;
                case EffectType.Buf:
                    if (target == TargetType.SingleTarget)
                    {
                        if (fun.StatMod == Function.TargetStat.Dmg)
                        {
                            e_dmg += var.Dmg;
                        }
                        else
                        {
                            e_def += (int)System.Math.Round((pc._def * var.Dmg / 100f));
                        }
                    }
                    else
                    {

                    }
                    break;
            }
        }
        else
        {
            Consumibles con = code.con_reference;
            EffectType con_e = con.cons_effect;
            TargetType con_t = con.cons_target;
            switch (con_e)
            {
                case EffectType.Damage:
                    if (con_t == TargetType.SingleTarget)
                    {
                        if (con.Multihit){for (int i = 0; i < con.Hits; i++){n_enemy[0].ChangeHp(con.cons_value, con.Magic);}}
                        else{n_enemy[0].ChangeHp(con.cons_value, con.Magic);}
                        CheckDead();
                    }
                    else//AOE
                    {
                        foreach (Enemy t in n_enemy)
                        {
                            if (con.Multihit)
                            {
                                for (int x = 0; x < con.Hits; x++){t.ChangeHp(con.cons_value, con.Magic);}
                            }
                            else{t.ChangeHp(con.cons_value, con.Magic);}
                        }
                        CheckDead();
                    }
                    break;
                case EffectType.Healing:
                    if (con_t == TargetType.SingleTarget)
                    {
                        
                    }
                    else
                    {

                    }
                    break;
                case EffectType.Buf:
                    if (con_t == TargetType.SingleTarget)
                    {
                        if (con.BuffAtk){e_dmg += con.cons_value;}
                        else if(con.BuffDef){e_def += (int)System.Math.Round((pc._def * con.cons_value / 100f));}
                        else
                        {
                            e_cost += con.cons_value;
                            UpdateCostMod();
                        }
                    }
                    break;
            }
            con.Uses--;
            UpdateConsumibles();
        }
        
    }
    private void UpdateConsumibles()
    {
        
    }
    public void UpdateActions()
    {
        Current_cost = 0;
        ActionsInBoard.Clear();
        foreach (Transform child in Board)
        {
            if(child.GetComponent<Drag_Drop>() != null)
            {
                var x = child.GetComponent<Drag_Drop>();
                if (x.var_ref != null)
                {
                    ActionsInBoard.Add(new Actions(x.itemdata, x.var_ref.itemdata, null));
                    if (x.itemdata.magic && !x.var_ref.itemdata.IsMagic) { 
                        Current_cost += (x.itemdata.ItemCost + 1); }
                    else if (!x.itemdata.magic && x.var_ref.itemdata.IsMagic) {
                        Current_cost += (x.itemdata.ItemCost + 1);}
                    else { Current_cost += x.itemdata.ItemCost; }
                }
                else{Current_cost += x.itemdata.ItemCost;}
            }
            else if (child.GetComponent<Consumible_Handler>() != null)
            {
                ActionsInBoard.Add(new Actions(null, null,child.GetComponent<Consumible_Handler>().itemdata));
                Current_cost += child.GetComponent<Consumible_Handler>().itemdata.ItemCost;
            }
        }
        CostCounter.text = "Cost:"+Current_cost;
    }
    
    public void StatModifier(int multiplier, bool positive, int Stat)
    {
        if (positive)
        {
            switch (Stat)
            {
                case 0://attack
                    e_dmg += multiplier;
                    break;
                case 1://def
                    e_def += (int)System.Math.Round((pc._def * multiplier / 100f));
                    break;
                case 2://maxcost
                    //
                    break;
            }
        }
        else
        {
            switch (Stat)
            {
                case 0://attack
                    e_dmg -= multiplier;
                    break;
                case 1://def
                    e_def -= (int)System.Math.Round((pc._def * multiplier / 100f));
                    break;
                case 2://maxcost
                    //
                    break;
            }
        }
    }
    //Combat preparation and initialization
    void FillInventory()
    {
        foreach (Function f in pc._inventory.functions)
        {
            var temp = Instantiate(fun_prefab, fun_inv);
            temp.GetComponent<Drag_Drop>().itemdata = f;
            temp.GetComponent<Drag_Drop>().UpdateData();
        }
        foreach (Variable v in pc._inventory.variables)
        {
            var temp = Instantiate(var_prefab, var_inv);
            temp.GetComponent<Variable_Handler>().itemdata = v;
        }
        foreach (Consumibles x in pc._inventory.consumibles)
        {
            var temp = Instantiate(cons_prefab, con_inv);
            temp.GetComponent<Consumible_Handler>().itemdata = x;
        }
        foreach (GameObject x in pc._inventory.used_consumibles)
        {
            Instantiate(x, con_inv);
        }
    }
    void SpawnEnemies()
    {
        int count = 0;
        foreach (GameObject x in GameObject.Find("EventSystem").GetComponent<SceneControl>().CurrentNode.Node_Enemies)//get enemy from node
        {
            var temp = Instantiate(x, enemy_parent.transform);
            n_enemy.Add(temp.GetComponent<Enemy>());
            temp.transform.localPosition = new Vector3(count * 1.5f, 0f, 0f);
            count++;
        }
    }
}
public class StatModifiers
{
    int value;
    int duration;
}
[System.Serializable]
public class Actions
{
    public Function fun_reference;
    public Variable var_reference;
    public Consumibles con_reference;
    public Actions(Function fun, Variable var, Consumibles cons)
    {
        fun_reference = fun;
        var_reference = var;
        con_reference = cons;
    }
}
