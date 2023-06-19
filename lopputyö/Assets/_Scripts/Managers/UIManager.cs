using UnityEngine;
using System.Collections;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject itemChooseUI;

    public void ActivateItemChoose(bool _value)
    {
        itemChooseUI.SetActive(_value);
    }
    
}
