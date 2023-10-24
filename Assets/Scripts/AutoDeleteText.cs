using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeleteText : MonoBehaviour
{
    [SerializeField] Animator anim;
    public TMPro.TextMeshPro _text;
    void Start()
    {
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
