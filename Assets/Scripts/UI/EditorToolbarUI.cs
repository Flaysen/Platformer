using LevelEditor;
using UnityEngine;

namespace UI
{
    public class EditorToolbarUI : MonoBehaviour
    {
        [SerializeField] private EditorToolbar _inventory;
        [SerializeField] private Transform _content;
        [SerializeField] private ToolbarItemUI _itemUIPrefab;

        private void Start()
        {
            Display(_inventory);
        }   

        public virtual void Display(EditorToolbar inventory)
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

            foreach(ToolbarItem item in _inventory.ToolbarItems)
            {
                ToolbarItemUI itemUI = Instantiate(_itemUIPrefab, _content);
                itemUI.Display(item);
            }
        }
    }
}



