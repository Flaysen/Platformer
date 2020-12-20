using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor 
{
    public class ObjectPlacer : MonoBehaviour
    {
        private Grid _grid;

        private void Awake()
        {
            _grid = FindObjectOfType<Grid>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if(Physics.Raycast(ray, out hitInfo))
                {
                    PlaceObjectNear(hitInfo.point);
                }
            }   
        }

        private void PlaceObjectNear(Vector3 clickPoint)
        {
            var finalPosition = _grid.GetNearestPointOnGrid(clickPoint);
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;  
        }
    }   
}

