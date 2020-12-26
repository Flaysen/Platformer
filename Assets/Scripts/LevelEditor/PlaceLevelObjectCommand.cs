using LevelEditor;

public class PlaceLevelObjectCommand : ICommand
{

    private LevelObject _levelObject;
    private GridPosition _gridPosition;
    private Grid _grid;
    
    public PlaceLevelObjectCommand(LevelObject levelObject, GridPosition gridPosition, Grid grid)
    {
        _levelObject = levelObject;
        _gridPosition = gridPosition;
        _grid = grid;
    }

    public void Execute()
    {
        ObjectPlacer placer = new ObjectPlacer();
        placer.PlaceObject(_levelObject, _gridPosition, _grid);
    }
}
