using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;

    [Tooltip("World Grid Size shows UnityEditor snap settings")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize {get {return unityGridSize;}}

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        //float gridWorld = UnityEditor.EditorSnapSettings.move.x; //var for dividing x grid position
        //float snapMoveY = UnityEditor.EditorSnapSettings.move.z; //var for dividing z grid position
        coordinates.x = (int)(position.x / unityGridSize);
        coordinates.y = (int)(position.z / unityGridSize);
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        position.x = (int)(coordinates.x * unityGridSize);
        position.z = (int)(coordinates.y * unityGridSize);
        return position;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int indexedCoordinates  = new Vector2Int(x, y);
                grid.Add(indexedCoordinates, new Node(indexedCoordinates, true));
                //Debug.Log(grid[indexedCoordinates].coordinates + " = " + grid[indexedCoordinates].isWalkable);
            }
        }
    }


}
