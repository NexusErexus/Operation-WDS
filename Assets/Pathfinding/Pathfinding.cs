using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    [SerializeField] Vector2Int endCoords;
    
    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>(); //границы которые нужно исследовать 
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>(); //словарь чтобы узнать исследован ли был узел 

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    // Start is called before the first frame update
    /*private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }

        
    }*/

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            endNode = grid[endCoords];
            
            StartCoroutine(BreadthFirstSearch());
        }
        

        
        //BreadthFirstSearch();
    }

    void ExploreNeighbours()
    {
        List<Node> neighbors = new List<Node>(); // лист с соседними клетками
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction; //кординаты соседних клеток, текущий + направление
            //currentSearchNode = gridManager.GetNode(neighborCoordinates);
            if (grid.ContainsKey(neighborCoordinates)) // если координаты соседей существуют
            {
                
                neighbors.Add(grid[neighborCoordinates]); // добавить на сетку координаты соседей
                //grid[startCoords].isExplored = true; //перекрасить нулевую координату
                //grid[currentSearchNode.coordinates].isPath = true;
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable) //если словарь не содержит координаты текущего соседа и может передвигаться 
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor); //добавляем координаты соседа
                frontier.Enqueue(neighbor); // добавить в конец очереди границы
            }
        }
    }

    IEnumerator BreadthFirstSearch() //алгоритм поиска пути
    {
        bool isRunning = true;
        frontier.Enqueue(startNode); //добавить начальный узел в конец очереди
        reached.Add(startCoords, startNode); //добавить координаты и начальный узел в список исследованных узлов

        while(frontier.Count > 0 && isRunning) // пока узлов в дереве больше 0
        {
            currentSearchNode = frontier.Dequeue(); //убрать текущий узел из очереди и пометить текущий узел первым в очереди
            currentSearchNode.isExplored = true; //пометить текущий узел как пройденный
            
            ExploreNeighbours();
            if (currentSearchNode.coordinates == endCoords) // если координаты текущего узла равно конечного узлу
            {
                isRunning = false;
            }
            Debug.Log(currentSearchNode.coordinates);
            yield return null;
        }
        BuildPath();
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        return path;
    }
}
