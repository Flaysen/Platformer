using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor 
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToPlace;
        private GameObject _selectedObject;


        private Grid _grid;
        private void Awake()
        {
            _grid = FindObjectOfType<Grid>();
            _selectedObject = Instantiate(_objectToPlace, transform.position, Quaternion.identity, transform); 
            Color color = _selectedObject.GetComponent<MeshRenderer>().material.color;
            color = new Color(color.r, color.g, color.b, 0.1f); 
            _selectedObject.GetComponent<MeshRenderer>().material.color = color;   

        }

        // Update is called once per frame
        void Update()
        {

            RaycastHit hitInfoHover;
            Ray rayHover = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(rayHover, out hitInfoHover))
            {
                _selectedObject.transform.position = _grid.GetNearestPointOnGrid(hitInfoHover.point);
            }

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

            Instantiate(_objectToPlace, finalPosition, Quaternion.identity, _grid.transform);

            //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;  
        }
    }   
}

