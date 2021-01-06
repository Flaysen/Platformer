using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Item _item;

    [SerializeField] private Text _itemName;
    
    private void Awake()
    {
        _itemName = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        if(_item)
        {
            Display(_item);
        }     
    }

    public virtual void Display(Item item)
    {
        this._item = item;
        _itemName.text = item.name;
    }


}
