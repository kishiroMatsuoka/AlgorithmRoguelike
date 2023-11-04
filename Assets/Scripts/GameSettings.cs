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
                Screen.SetResolution(2560, 1440, true);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 3:
                Screen.SetResolution(1366, 768, true);
                break;
            case 4:
                Screen.SetResolution(1280, 720, true);
                break;
            default:
                break;
        }
        
    }
}
