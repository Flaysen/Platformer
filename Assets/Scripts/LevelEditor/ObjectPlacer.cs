using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                        //PlaceObject(selectedObjectGridPosition);
                        ICommand command = new PlaceLevelObjectCommand(_objectToPlace, selectedObjectGridPosition, _grid);
                        CommandInvoker.AddCommand(command);
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
        public void PlaceLeveleObject(LevelObject objectToPlace, GridPosition gridPosition, Grid grid)
        {  
            grid.ToggleGridPositionUsage(gridPosition);
            LevelObject placedLevelObject = Instantiate(objectToPlace, gridPosition.Position, Quaternion.identity, grid.transform);       
            Grid.LevelObjects.Add(placedLevelObject);
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

        public void RemoveLevelObject(GridPosition gridPosition, Grid grid)
        {
            //evelObject levelObjectToRemove = Grid.LevelObjects.Where(l => l.transform.position == gridPosition.Position).FirstOrDefault();

            for(int i = 0; i < Grid.LevelObjects.Count; i++)
            {
                if (Grid.LevelObjects[i].transform.position == gridPosition.Position)
                { 
                    grid.ToggleGridPositionUsage(gridPosition);
                    Destroy(Grid.LevelObjects[i].gameObject);  
                    Grid.LevelObjects.RemoveAt(i);
                    return;  
                }
            }            
        }      
    }   
}

