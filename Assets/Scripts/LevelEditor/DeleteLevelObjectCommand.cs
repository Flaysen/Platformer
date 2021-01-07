using Core;

namespace LevelEditor
{
    public class DeleteLevelObjectCommand : ICommand
    {
        private LevelObject _levelObject;
        private GridPosition _gridPosition;
        private LevelGrid _grid;
        private ObjectPlacer _placer;

        public DeleteLevelObjectCommand(LevelObject levelObject, GridPosition gridPosition, LevelGrid grid)
        {
            _levelObject = levelObject;
            _gridPosition = gridPosition;
            _grid = grid;

            _placer = new ObjectPlacer();
        }

        public void Execute()
        {
            _placer.RemoveLevelObject(_gridPosition, _grid);
        }
        
        public void Undo()
        {
            _placer.PlaceLeveleObject(_levelObject, _gridPosition, _grid);
        }
    }
}


