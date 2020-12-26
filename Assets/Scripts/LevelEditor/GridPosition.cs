using UnityEngine;

namespace LevelEditor
{
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
}