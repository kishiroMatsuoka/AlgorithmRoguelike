using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionController : MonoBehaviour
{
    [SerializeField] GameObject inv, pz;
    private void Start()
    {
        CheckZone();
    }
    public bool InInventory()
    {
        return inv.activeSelf;
    }
    public void CheckZone()
    {
        switch (transform.parent.GetComponent<Attach_Zone>().inventory_zone)
        {
            case Attach_Zone.inventoryType.inventory:
                inv.SetActive(true);
                pz.SetActive(false);
                break;
            case Attach_Zone.inventoryType.Ejecucion:
                pz.SetActive(true);
                inv.SetActive(false);
                break;
        }
    }
}
