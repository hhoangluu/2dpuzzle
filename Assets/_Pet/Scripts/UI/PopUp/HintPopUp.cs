using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintPopUp : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    public void SetHintText(string text)
    {
        
    }

    public void Show(string hint)
    {
        this.text.text = hint;
        gameObject.SetActive(true);
    }
}
