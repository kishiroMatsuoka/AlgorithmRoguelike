using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using ItemSystem;

public class Drag_Drop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] UnityEngine.UI.Image spriteinv, spritegame;
    [SerializeField] TextMeshProUGUI InvName, Description;
    [HideInInspector] public Transform Parent_Transform, CanvasT;
    [HideInInspector] public Attach_Zone varAttach;
    [HideInInspector] public Function itemdata;
    [HideInInspector] public Variable_Handler var_ref = null;
    //drag variables
    Vector3 _position;
    Combat_controller cc;
    FunctionController fc;
    //info variables
    bool desc_updated = false;
    private float timer = 0f;
    private readonly float timerend = 1.5f;
    bool isHovering = false;
    void Start()
    {
        fc = GetComponent<FunctionController>();
        _position = transform.position;
        CanvasT = GameObject.Find("Combat UI").GetComponent<Transform>();
        cc = FindObjectOfType<Combat_controller>();
        spriteinv.sprite = itemdata.ItemSprite;
        spritegame.sprite = spriteinv.sprite;

    }
    private void Update()
    {
        if (cc.Dragging && transform.parent != cc.Board)
        {
            if(GetComponent<CanvasGroup>().blocksRaycasts)
            {
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        else if(!GetComponent<CanvasGroup>().blocksRaycasts)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        if (isHovering)
        {
            if (timer >= timerend)
            {
                if (!desc_updated)
                {
                    cc.itemdesc.SetActive(true);
                    GameObject.Find("Description").GetComponent<TextMeshProUGUI>().text = itemdata.ItemDescription;
                    desc_updated = true;
                }
                Vector3 mousepos = Input.mousePosition;
                mousepos.z = -6f;
                //print(mousepos);
                cc.itemdesc.transform.position = mousepos;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    public void Hovering()
    {
        if (fc.InInventory())
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
        cc.itemdesc.SetActive(false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Parent_Transform = transform.parent;
        transform.SetParent(CanvasT);
        gameObject.layer = 6;
        cc.Dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cc.Dragging = false;
        gameObject.layer = 5;
        if (transform.parent == CanvasT)
        {
            transform.SetParent(Parent_Transform);
            transform.position = _position;
        }
    }
    public void UpdateData()
    {
        InvName.text = itemdata.ItemName;
        Description.text = itemdata.ItemDescription;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var t = eventData.pointerDrag;
        if (transform.parent == cc.Board)
        {
            if(t.GetComponent<Drag_Drop>() != null ||
                t.GetComponent<Consumible_Handler>() != null)
            {
                cc.Board.GetComponent<Attach_Zone>().OnDrop(eventData);
                t.transform.SetSiblingIndex(transform.GetSiblingIndex());
            }
        }
    }
}
