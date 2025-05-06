using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private Image _textIcon;
    [SerializeField] private GameObject _stackObject;
    [SerializeField] private TextMeshProUGUI _stackNumber;

    public void Set(InventoryItem item )
    {
        _textName.text = item.data.displayName;
        _textIcon.sprite = item.data.icon;

        if (item.stackSize >= 1)
        {
            _stackObject.SetActive(true);
        }
        else
        {
            _stackObject.SetActive(false);
            return;
        }

        _stackNumber.text = item.stackSize.ToString();
    }
}
