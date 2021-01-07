using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToolbarItemUI : MonoBehaviour
    {
        [SerializeField] private ToolbarItem _item;
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

        public virtual void Display(ToolbarItem item)
        {
            _item = item;
            _itemName.text = item.name;
        }
    }
}

