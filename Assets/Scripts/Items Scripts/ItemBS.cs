using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBS : MonoBehaviour, IDropHandler
{
    [SerializeField] StoreController sc;
    Player_Controller pc;
    public bool Sale;
    void Start()
    {
        pc = FindObjectOfType<Player_Controller>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var b = eventData.pointerDrag.GetComponent<ItemStore>();
        if (Sale)
        {
            if (b.Own)
            {
                pc.money += b.price;
                var i = eventData.pointerDrag.GetComponent<LootPreview>().itemdata;
                pc._inventory.RemoveFromInventory(i);
                sc.Ventas++;
                Destroy(eventData.pointerDrag);
            }
        }
        else//buy
        {
            
            if (!b.Own)
            {
                if ((pc.money - b.price) >= 0)
                {
                    pc.money -= b.price;
                    var i = eventData.pointerDrag.GetComponent<LootPreview>().itemdata;
                    pc._inventory.AddToInventory(i);
                    sc.Compras++;
                    Destroy(eventData.pointerDrag);
                }
            }
        }
    }
}
