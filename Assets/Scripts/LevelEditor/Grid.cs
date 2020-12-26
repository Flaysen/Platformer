using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelEditor
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private float _offset = 1;
        private List<GridPosition> _gridPositions = new List<GridPosition>();

        private void Awake()
        {
            InitializeGridPositions();

            CreatePlane();
        }

        public void ToggleGridPositionUsage(GridPosition gridPosition)
        {           
            GridPosition positionToUse = GetGridPosition(gridPosition);

            positionToUse.IsUsed = !positionToUse.IsUsed;
        }

        public bool CheckIfGridPositionIsValid(GridPosition gridPosition)
        {
            GridPosition positionToUse = GetGridPosition(gridPosition);

            if(positionToUse != null && !positionToUse.IsUsed)
            {
                return true;
            }
            return false;
        }

        private GridPosition GetGridPosition(GridPosition gridPosition)
        {
            return _gridPositions.Where(gP => gP.Position == gridPosition.Position).FirstOrDefault();
        }

        public GridPosition GetNearestPointOnGrid(Vector3 position)
        {
            position -= transform.position;

            Vector3 finalPosition = new Vector3(
                Mathf.RoundToInt(position.x / _offset) * _offset,
                Mathf.RoundToInt(position.y / _offset) * _offset,
                Mathf.RoundToInt(position.z / _offset) *_offset);

            finalPosition += transform.position;   

            GridPosition result = new GridPosition(finalPosition);
    
            return result;         
        }

        private void CreatePlane()
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.parent = transform;

            plane.transform.localScale = new Vector3(_size.x / 10, 1, _size.z / 10); 
            plane.transform.position = new Vector3((_size.x - 1) / 2, 0, (_size.z - 1) / 2);  

            plane.GetComponent<MeshRenderer>().material.color = Color.gray;
        }

        private void InitializeGridPositions()
        {
            for (int x = 0; x < _size.x; x++)
            {
                for(int y = 0; y < _size.y; y++)
                {
                    for(int z = 0; z < _size.z; z++)
                    {
                        _gridPositions.Add(new GridPosition(new Vector3(x, y, z)));
                    }
                }
            }
        }
    }     
}


