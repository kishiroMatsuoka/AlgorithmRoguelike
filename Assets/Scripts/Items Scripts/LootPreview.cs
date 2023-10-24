using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootPreview : MonoBehaviour
{
    public ItemSystem.Items itemdata;
    [SerializeField] GameObject itemname,iSprite,description,text;
    //info variables
    bool desc_updated = false;
    private float timer = 0f;
    private readonly float timerend = 1f;
    bool isHovering = false;
    public void SetName(ItemSystem.Items item)
    {
        itemdata = item;
        itemname.GetComponent<TextMeshProUGUI>().text = itemdata.ItemName;
        //iSprite.GetComponent<UnityEngine.UI.Image>().sprite = itemdata.ItemSprite;

    }
    private void Update()
    {
        if (isHovering)
        {
            if (timer >= timerend)
            {
                if (!desc_updated)
                {
                    description.SetActive(true);
                    text.GetComponent<TextMeshProUGUI>().text = itemdata.ItemDescription;
                    desc_updated = true;
                }
                //Vector3 mousepos = Input.mousePosition;
                //mousepos.z = -6f;
                //print(mousepos);
                //description.transform.position = mousepos;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    public void Hovering()
    {
        isHovering = true;
    }
    public void MouseExit()
    {
        //print("exit called");
        timer = 0f;
        isHovering = false;
        desc_updated = false;
        description.SetActive(false);
    }
}
