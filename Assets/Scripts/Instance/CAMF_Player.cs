using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMF_Player : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coord = transform.position;
        coord.y = player.transform.position.y;
        transform.position = coord;
    }
}
