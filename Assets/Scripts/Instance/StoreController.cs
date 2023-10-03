using UnityEngine;
using TMPro;

public class StoreController : MonoBehaviour
{
    //storepool add here
    [SerializeField] TextMeshProUGUI health, gold;
    public Transform PanelVenta, PanelInventario, Stack;
    public GameObject Itemdesc;
    Player_Controller pc;
    private void OnEnable()
    {
        pc = GameObject.Find("Player").GetComponent<Player_Controller>();
        health.text = pc._hp.ToString();
        gold.text = pc.money.ToString();
    }
    public void FillStore()
    {
        foreach(Transform slot in PanelVenta)
        {

        }
    }

}
