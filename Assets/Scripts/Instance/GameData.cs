using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] GameObject[] _player;
    void CreatePC(int pc_class)
    {
        var x = Instantiate(_player[pc_class]);
        x.name = "Player";
    }
}
