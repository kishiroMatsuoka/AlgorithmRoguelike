using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using ItemSystem;

public class Consumible_Handler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform Parent_Transform, CanvasT;
    public Consumibles itemdata;
    Vector3 _position;
    public int _uses;
    public bool Used = false;
    [SerializeField] TextMeshProUGUI InvName, Description;
    Combat_controller cc;
    bool desc_updated = false;
    private float timer = 0f;
    private readonly float timerend = 2.0f;

    void Start()
    {
        _uses = itemdata.Uses;
        _position = transform.position;
        CanvasT = GameObject.Find("Combat UI").transform;
        cc = FindObjectOfType<Combat_controller>();
    }
    bool isHovering = false;
    private void Update()
    {
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
        isHovering = true;
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
        if (!Used)
        {
            Parent_Transform = transform.parent;
            transform.SetParent(CanvasT);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        else
        {
            eventData.pointerDrag = null;
        }
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
