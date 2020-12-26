using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor 
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private LevelObject _objectToPlace;
        private LevelObject _selectedObject;
        private Grid _grid;
        private Camera _camera;
        private bool _isPositionValid;
     
        private void Awake()
        {
            _grid = FindObjectOfType<Grid>();

            _camera = FindObjectOfType<Camera>();

            CreatePlacerPointer();
        }

        void Update()
        {
            HandleObjectPlacement();   
        }
        private void HandleObjectPlacement()
        {
            RaycastHit hitInfo;
            Ray rayHover = _camera.ScreenPointToRay(Input.mousePosition);
            GridPosition selectedObjectGridPosition;

            if(Physics.Raycast(rayHover, out hitInfo))
            {     
                selectedObjectGridPosition = _grid.GetNearestPointOnGrid(hitInfo.point);
                _isPositionValid =  _grid.CheckIfGridPositionIsValid(selectedObjectGridPosition);

                _selectedObject.transform.position = selectedObjectGridPosition.Position;
                _selectedObject.SetVisability(_isPositionValid);

                if(Input.GetMouseButtonDown(0))
                {
                    if(_isPositionValid)
                    {                      
                        PlaceObject(selectedObjectGridPosition);
                    }                  
                }  
  
                if(Input.GetMouseButtonDown(1))
                {      
                    DeleteObject(hitInfo.collider.transform);                             
                }     
            }    
        }
        private void CreatePlacerPointer()
        {
            _selectedObject = Instantiate(_objectToPlace, transform.position, Quaternion.identity, transform); 
            _selectedObject.SetTransparency(true);
            _selectedObject.GetComponent<Collider>().enabled = false;           
        }
        private void PlaceObject(GridPosition gridPosition)
        {  
            _grid.ToggleGridPositionUsage(gridPosition);
            Instantiate(_objectToPlace, gridPosition.Position, Quaternion.identity, _grid.transform);       
        }

        private void DeleteObject(Transform transform)
        {
            if(transform.GetComponent<LevelObject>())
            {            
                GridPosition finalPosition = _grid.GetNearestPointOnGrid(transform.position);
                _grid.ToggleGridPositionUsage(finalPosition);
                Destroy(transform.gameObject); 
            }         
        }
    }   
}

