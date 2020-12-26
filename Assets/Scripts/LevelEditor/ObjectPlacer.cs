using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor 
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToPlace;
        private GameObject _selectedObject;
        private bool _isPositionValid;


        private Grid _grid;
        private void Awake()
        {
            _grid = FindObjectOfType<Grid>();
            _selectedObject = Instantiate(_objectToPlace, transform.position, Quaternion.identity, transform); 
            _selectedObject.GetComponent<LevelObject>().ToggleHover();
            _selectedObject.GetComponent<Collider>().enabled = false;   

        }

        // Update is called once per frame
        void Update()
        {

            RaycastHit hitInfoHover;
            Ray rayHover = Camera.main.ScreenPointToRay(Input.mousePosition);
            Transform selectedTransform = _selectedObject.transform;

            if(Physics.Raycast(rayHover, out hitInfoHover))
            {
               
                selectedTransform.position = _grid.GetNearestPointOnGrid(hitInfoHover.point);
                 _isPositionValid =  _grid.CheckIfGridPositionIsValid(selectedTransform.position);
                _selectedObject.GetComponent<LevelObject>().SetVisability(_isPositionValid);
            }

             Debug.DrawLine(rayHover.origin, hitInfoHover.point, Color.blue);

            if(Input.GetMouseButtonDown(0))
            {       
                if(_isPositionValid)
                {
                    PlaceObject(selectedTransform.position);
                }              
            }   

            if(Input.GetMouseButtonDown(1))
            {
                if(true)
                {
                    DeleteObject(hitInfoHover.collider.transform);
                }           
            }
        }
        private void PlaceObject(Vector3 position)
        {  
            _grid.TogglePositionUsage(position);
            Instantiate(_objectToPlace, position, Quaternion.identity, _grid.transform);       
        }

        private void DeleteObject(Transform transform)
        {
            Debug.Log("A");
            if(transform.GetComponent<LevelObject>())
            {
            Debug.Log("B");
                
                Vector3 finalPosition = _grid.GetNearestPointOnGrid(transform.position);
                _grid.TogglePositionUsage(finalPosition);
                Destroy(transform.gameObject); 
            }         
        }
    }   
}

