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
        private LevelGrid _grid;
        private Camera _camera;
        private bool _isPositionValid;
     
        private void Awake()
        {
            _grid = FindObjectOfType<LevelGrid>();
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
                        ICommand command = new PlaceLevelObjectCommand(_objectToPlace, selectedObjectGridPosition, _grid);
                        CommandInvoker.AddCommand(command);
                    }                  
                }  
  
                if(Input.GetMouseButtonDown(1))
                {      
                    LevelObject objectToDelete = hitInfo.collider.transform.GetComponent<LevelObject>();
                    if(objectToDelete != null)
                    {
                        ICommand command = new DeleteLevelObjectCommand(
                            objectToDelete,
                            _grid.GetNearestPointOnGrid(objectToDelete.transform.position),
                            _grid);

                        CommandInvoker.AddCommand(command);                
                    }                         
                }     
            }    
        }
        private void CreatePlacerPointer()
        {
            _selectedObject = Instantiate(_objectToPlace, transform.position, Quaternion.identity, transform); 
            _selectedObject.SetTransparency(true);
            _selectedObject.GetComponent<Collider>().enabled = false;           
        }
        public void PlaceLeveleObject(LevelObject objectToPlace, GridPosition gridPosition, LevelGrid grid)
        {  
            grid.ToggleGridPositionUsage(gridPosition);
            LevelObject placedLevelObject = Instantiate(objectToPlace, gridPosition.Position, Quaternion.identity, grid.transform);       
            LevelGrid.LevelObjects.Add(placedLevelObject);
            placedLevelObject.gameObject.SetActive(true);
       }

        public void DeleteLevelObject(LevelObject objectToPlace, GridPosition gridPosition, LevelGrid grid)
        {
           for(int i = 0; i < LevelGrid.LevelObjects.Count; i++)
            {
                if (LevelGrid.LevelObjects[i].transform.position == gridPosition.Position)
                { 
                    grid.ToggleGridPositionUsage(gridPosition);
                    LevelGrid.LevelObjects[i].gameObject.SetActive(false);
                    LevelGrid.LevelObjects.RemoveAt(i);
                    return;  
                }
            }            
        }

        public void RemoveLevelObject(GridPosition gridPosition, LevelGrid grid)
        {
            for(int i = 0; i < LevelGrid.LevelObjects.Count; i++)
            {
                if (LevelGrid.LevelObjects[i].transform.position == gridPosition.Position)
                { 
                    grid.ToggleGridPositionUsage(gridPosition);
                    LevelGrid.LevelObjects[i].gameObject.SetActive(false);
                    LevelGrid.LevelObjects.RemoveAt(i);
                    return;  
                }
            }            
        }      
    }   
}

