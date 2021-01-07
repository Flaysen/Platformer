using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EditorToolbar : MonoBehaviour
    {
        public List<ToolbarItem> ToolbarItems = new List<ToolbarItem>();

        private void Awake()
        {
            foreach(ToolbarItem item in GetComponentsInChildren<ToolbarItem>())
            {
                ToolbarItems.Add(item);
            }
        }
    }
}


