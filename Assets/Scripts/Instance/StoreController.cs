using UnityEngine;
using TMPro;

public class StoreController : MonoBehaviour
{
    //storepool add here
    [SerializeField] TextMeshProUGUI health, gold;
    public Transform PanelVenta, PanelInventario, Stack;
    public GameObject LootPrefab;
    Player_Controller pc;
    public int Compras, Ventas;
    private void OnEnable()
    {
        Compras = Ventas = 0;
        pc = GameObject.Find("Player").GetComponent<Player_Controller>();
        health.text = pc._hp.ToString();
        gold.text = pc.money.ToString();
        FillStore();
        SellOptions();
    }
    private void Update()
    {
        gold.text = pc.money.ToString();
    }
    void SellOptions()
    {
        foreach (Transform slot in PanelInventario)
        {
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0));
            }
        }
        foreach (Transform slot in PanelInventario)
        {
            var i = pc._inventory.GetRandomItem();
            if (i != null)
            {
                var t = Instantiate(LootPrefab, slot);
                t.GetComponent<ItemStore>().Own = true;
                t.GetComponent<LootPreview>().SetName(i);
            }
        }
        
    }
    public void Exit()
    {
        FindObjectOfType<SceneControl>().ExitStore(Compras, Ventas);
    }
    void FillStore()
    {
        foreach (Transform slot in PanelVenta)
        {
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0));
            }
        }
        foreach (Transform slot in PanelVenta)
        {
            var i = pc.Store.GetRandomItem(Random.Range(0, 10));
            var t = Instantiate(LootPrefab, slot);
            t.GetComponent<LootPreview>().SetName(i);
        }
    }

}
