using System.Collections;
using System.Collections.Generic;
using LevelEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    private void Awake()
    {
        foreach(Item item in GetComponentsInChildren<Item>())
        {
            Items.Add(item);
        }
    }
}
