using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attach_Zone : MonoBehaviour, IDropHandler
{
    public inventoryType inventory_zone;
    Combat_controller cc;
    bool Occupied = false;
    private void Start()
    {
        cc = GameObject.Find("CombatController").GetComponent<Combat_controller>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        //print("Dropped in inventory");
        var d = eventData.pointerDrag;
        switch (inventory_zone)
        {
            case inventoryType.Ejecucion:
                if (d.GetComponent<Drag_Drop>() != null)
                {
                    //check if cost is posible
                    if (cc.CheckCost(d.GetComponent<Drag_Drop>().itemdata.ItemCost))
                    {
                        d.transform.SetParent(transform);
                        d.GetComponent<FunctionController>().CheckZone();
                    }
                }
                else if(d.GetComponent<Consumible_Handler>() != null)
                {
                    var ch = d.GetComponent<Consumible_Handler>();
                    //check if cost is posible
                    if (cc.CheckCost(ch.itemdata.ItemCost))
                    {
                        if (ch.itemdata.cons_effect == ItemSystem.EffectType.Buf)
                        {
                            cc.CodeToExecute(new Actions(null, null, ch.itemdata));
                            ch.Used = true;
                        }
                        else
                        {
                            d.transform.SetParent(transform);
                            cc.ActionsInBoard.Add(new Actions(null, null, ch.itemdata));

                        }
                    }
                }
                break;
            case inventoryType.inventory:
                ToInventory(d);
                break;
            case inventoryType.Function:
                if (d.GetComponent<Variable_Handler>() != null && !Occupied)
                {
                    var handler = d.GetComponent<Variable_Handler>();
                    var item = transform.parent.parent.GetComponent<Drag_Drop>();
                    if (item.itemdata.Percent && handler.itemdata.isPercentage)
                    {
                        handler.function = item;
                        handler.function.var_ref = handler;
                        Occupied = true;
                        cc.ActionsInBoard.Add(new Actions(handler.function.itemdata, handler.function.var_ref.itemdata, null));
                        d.transform.SetParent(transform);
                    }
                    else
                    {
                        int cost = 0;
                        if(item.itemdata.magic && !handler.itemdata.IsMagic){cost = 1;}
                        else if (!item.itemdata.magic && handler.itemdata.IsMagic){cost = 1;}
                        if (cc.CheckCost(cost))
                        {
                            handler.function = transform.parent.parent.GetComponent<Drag_Drop>();
                            handler.function.var_ref = handler;
                            Occupied = true;
                            cc.ActionsInBoard.Add(new Actions(handler.function.itemdata, handler.function.var_ref.itemdata, null));
                            d.transform.SetParent(transform);
                        }
                    }
                }
                break;
        }
    }
    public void TurnFinished()
    {
        List<GameObject> childs = new List<GameObject>();
        foreach(Transform t in cc.Board)
        {
            childs.Add(t.gameObject);
        }
        foreach(GameObject c in childs)
        {
            ToInventory(c);
        }
    }
    public void ToInventory(GameObject g)
    {
        if (g.GetComponent<Drag_Drop>() != null)
        {
            var x = g.GetComponent<Drag_Drop>();
            if (x.var_ref != null)
            {
                x.varAttach.Occupied = false;
                x.var_ref.function = null;
                x.var_ref.transform.SetParent(cc.var_inv);
                x.var_ref = null;
            }
            g.transform.SetParent(cc.fun_inv);
            g.GetComponent<FunctionController>().CheckZone();
            
        }
        else if (g.GetComponent<Variable_Handler>() != null)
        {
            var x = g.GetComponent<Variable_Handler>();
            try { x.function.var_ref = null; } catch { }
            x.function = null;
            x.Parent_Transform.GetComponent<Attach_Zone>().Occupied = false;
            g.transform.SetParent(cc.var_inv);
        }
        else if (g.GetComponent<Consumible_Handler>() != null)
        {
            g.transform.SetParent(cc.con_inv);
        }
        //calls update actions
        cc.UpdateActions();
    }
    public enum inventoryType
    {
        Ejecucion,
        inventory,
        Function
    }
}


