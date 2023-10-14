using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ItemStore : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] TextMeshProUGUI pricetxt;
    Transform Parent_Transform,CanvasStore;
    Vector3 _position;
    public int price;
    public bool Own = false;
    private void Start()
    {
        price = GetComponent<LootPreview>().itemdata.ItemRarity * 68;
        pricetxt.text = price.ToString();
        CanvasStore = GameObject.Find("StoreUI").transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Parent_Transform = transform.parent;
        transform.parent = CanvasStore;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == CanvasStore)
        {
            transform.SetParent(Parent_Transform);
            transform.position = _position;
        }
    }

}
