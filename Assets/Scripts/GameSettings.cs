using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSettings : MonoBehaviour
{
    public TMP_Dropdown ResolutionSelection;

    private void Start()
    {
        ResolutionSelection.onValueChanged.AddListener(
            delegate { ResolutionChanged(ResolutionSelection); });

    }
    void ResolutionChanged(TMP_Dropdown dropdown)
    {
        
        int index = dropdown.value;
        Debug.Log("Is Called, Index: "+index);
        switch (index)
        {
            case 1:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 2:
                Screen.SetResolution(1080, 720, true);
                break;
            case 3:
                Screen.SetResolution(640, 480, true);
                break;
        }
        
    }
}
