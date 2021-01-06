using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _content;
    [SerializeField] private ItemUI _itemUIPrefab;

    private void Start()
    {
        Display(_inventory);
    }   

    public virtual void Display(Inventory inventory)
    {
        _inventory = inventory;
        Refresh();
    }

    public virtual void Refresh()
    {
        foreach(Transform transform in _content)
        {
           Destroy(transform.gameObject); 
        }

        foreach(Item item in _inventory.Items)
        {
           ItemUI itemUI = Instantiate(_itemUIPrefab, _content);
           itemUI.Display(item);
        }
    }
    
}
