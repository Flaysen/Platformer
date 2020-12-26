using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    //public bool IsVisable { get; set; }

    private MeshRenderer _meshRenderer;
    private void Awake()
    {
        _meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    } 

    public void ToggleHover()
    {
        Color color = _meshRenderer.material.color;
        color = new Color(color.r, color.g, color.b, 0.1f); 
        _meshRenderer.material.color = color;
    }

    public void SetVisability(bool isVisible)
    {
        _meshRenderer.enabled = isVisible;
    }

}
