using UnityEngine;

namespace LevelEditor
{
    public class LevelObject : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private void Awake()
        {
            _meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        } 

        public void SetTransparency(bool isTransparent)
        {
            Color color = _meshRenderer.material.color;
            _meshRenderer.material.color = new Color(color.r, color.g, color.b, 0.1f);
        }

        public void SetVisability(bool isVisible)
        {
            _meshRenderer.enabled = isVisible;
        }
    }
}


