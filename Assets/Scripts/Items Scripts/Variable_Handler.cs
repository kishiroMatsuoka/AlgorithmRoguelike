using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using ItemSystem;
using UnityEngine.UI;

public class Variable_Handler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform Parent_Transform, CanvasT;
    [SerializeField] Image icon;
    [SerializeField] private TextMeshProUGUI txt;
    public Variable itemdata;
    public Drag_Drop function = null;
    Combat_controller cc;
    Vector3 _position;
    void Start()
    {
        txt.text = itemdata.Dmg.ToString();
        icon.sprite = itemdata.icon;
        _position = transform.position;
        CanvasT = GameObject.Find("Combat UI").GetComponent<Transform>();
        cc = FindObjectOfType<Combat_controller>();
    }
    private void Update()
    {
        if (cc.Dragging)
        {
            if (GetComponent<CanvasGroup>().blocksRaycasts)
            {
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        else if (!GetComponent<CanvasGroup>().blocksRaycasts)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
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
}
