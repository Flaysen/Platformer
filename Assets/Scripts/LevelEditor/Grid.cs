using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridPosition
{
    public Vector3 Position { get; private set; }
    public bool IsUsed { get; set; }

    public GridPosition(Vector3 position)
    {
        Position = position;
        IsUsed = false;
    } 
} 

namespace LevelEditor
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private float _offset = 1f;
        private List<GridPosition> _gridPositions = new List<GridPosition>();

        private void Awake()
        {
            GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.parent = transform;
            plane.transform.localScale = new Vector3(_size.x / 10f, 1f, _size.z / 10f); 
            plane.transform.position = new Vector3((_size.x - 1) / 2, 0f, (_size.z - 1) / 2);  
            plane.GetComponent<MeshRenderer>().material.color = Color.gray;

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

        public void TogglePositionUsage(Vector3 position)
        {
            GridPosition gridPosition = _gridPositions.Where(gP => gP.Position == position).FirstOrDefault();
            
            gridPosition.IsUsed = !gridPosition.IsUsed;
        }

        public bool CheckIfGridPositionIsValid(Vector3 position)
        {
            GridPosition positionToUse = _gridPositions.Where(gP => gP.Position == position).FirstOrDefault();
      
            if(positionToUse != null && !positionToUse.IsUsed)
            {
                return true;
            }
            return false;
        }

        public Vector3 GetNearestPointOnGrid(Vector3 position)
        {
            position -= transform.position;

            int xCount = Mathf.RoundToInt(position.x / _offset);
            int yCount = Mathf.RoundToInt(position.y / _offset);
            int zCount = Mathf.RoundToInt(position.z / _offset);

            Vector3 result = new Vector3(
                (float)xCount * _offset,
                (float)yCount * _offset,
                (float)zCount * _offset);

            result += transform.position;   

            return result;         
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     for (float x = 0; x < _size.x; x += _offset)
        //     {
        //         for(float z = 0; z < _size.z; z += _offset)
        //         {
        //             for(float y = 0; y < _size.y; y += _offset)
        //             {
        //                 var point = GetNearestPointOnGrid(new Vector3(x, y, z));
        //                 Gizmos.DrawSphere(point, 0.1f);
        //             }
        //         }
        //     }
        // }
    }     
}


