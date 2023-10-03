using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;
using UnityEngine.EventSystems;
using TMPro;
public class StoreItemcontroller : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Items data;
    public TextMeshProUGUI itemname,price;
    public Transform Parent_Transform, CanvasT;
    GameObject itemdesc;
    StoreController sc;
    //drag variables
    Vector3 _position;
    bool desc_updated = false;
    private float timer = 0f;
    private readonly float timerend = 1.5f;
    bool isHovering = false;
    private void Start()
    {
        CanvasT = GameObject.Find("StoreUI").GetComponent<Transform>();
        _position = transform.position;
        sc = FindObjectOfType<StoreController>();
        itemdesc = sc.Itemdesc;
    }
    private void Update()
    {
        if (isHovering)
        {
            if (timer >= timerend)
            {
                if (!desc_updated)
                {
                    itemdesc.SetActive(true);
                    GameObject.Find("Description").GetComponent<TextMeshProUGUI>().text = data.ItemDescription;
                    desc_updated = true;
                }
                Vector3 mousepos = Input.mousePosition;
                mousepos.z = -6f;
                //print(mousepos);
                itemdesc.transform.position = mousepos;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    public void Hovering()
    {
        if (transform.parent == sc.PanelVenta)
        {
            isHovering = true;
        }
    }
    public void MouseExit()
    {
        //print("exit called");
        timer = 0f;
        isHovering = false;
        desc_updated = false;
        itemdesc.SetActive(false);

    }
    public void UpdateData()
    {
        itemname.text = data.ItemName;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Parent_Transform = transform.parent;
        transform.SetParent(CanvasT);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public  void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == CanvasT)
        {
            transform.SetParent(Parent_Transform);
            transform.position = _position;
        }
    }
}
