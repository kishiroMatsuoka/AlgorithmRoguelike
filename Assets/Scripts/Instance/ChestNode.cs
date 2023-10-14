using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestNode : MonoBehaviour
{
    [SerializeField] GameObject PreviewPrefab, Reward, Puzle;
    [SerializeField] Transform Slot;
    void OnEnable()
    {
        Result(Random.Range(0, 2));
    }
    void Result(int x)
    {
        if(x == 1)
        {
            Puzle.SetActive(true);
        }
        else
        {
            Reward.SetActive(true);
            var temp = FindObjectOfType<Player_Controller>();
            var item = temp.General.GetRandomItem(2);
            temp._inventory.AddToInventory(item);
            var prev = Instantiate(PreviewPrefab, Slot);
            prev.GetComponent<LootPreview>().itemdata = item;
            prev.name = "rewardPreview";
        }
    }
    public void ResetUi()
    {
        GameObject x = GameObject.Find("rewardPreview");
        if(x!= null){Destroy(x);}
        Reward.SetActive(false);
        Puzle.SetActive(false);
        gameObject.SetActive(false);
    }
}
