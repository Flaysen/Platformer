using LevelEditor;

public class PlaceLevelObjectCommand : ICommand
{

    private LevelObject _levelObject;
    private GridPosition _gridPosition;
    private Grid _grid;
    private ObjectPlacer _placer;
    
    public PlaceLevelObjectCommand(LevelObject levelObject, GridPosition gridPosition, Grid grid)
    {
        _levelObject = levelObject;
        _gridPosition = gridPosition;
        _grid = grid;

        _placer = new ObjectPlacer();
    }

    public void Execute()
    {
        
        _placer.PlaceLeveleObject(_levelObject, _gridPosition, _grid);
    }
    public void Undo()
    {
        _placer.RemoveLevelObject(_gridPosition, _grid);
    }
}
