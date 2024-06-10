using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCountUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _keyCountUI;
    
    public void UpdateKeyCount(KeyController keyController)
    {
        _keyCountUI.text = keyController.keyCount.ToString();
    }
}
