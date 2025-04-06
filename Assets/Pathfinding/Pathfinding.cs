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

    Queue<Node> frontier = new Queue<Node>(); //������� ������� ����� ����������� 
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>(); //������� ����� ������ ���������� �� ��� ���� 

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
        List<Node> neighbors = new List<Node>(); // ���� � ��������� ��������
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction; //��������� �������� ������, ������� + �����������
            //currentSearchNode = gridManager.GetNode(neighborCoordinates);
            if (grid.ContainsKey(neighborCoordinates)) // ���� ���������� ������� ����������
            {
                
                neighbors.Add(grid[neighborCoordinates]); // �������� �� ����� ���������� �������
                //grid[startCoords].isExplored = true; //����������� ������� ����������
                //grid[currentSearchNode.coordinates].isPath = true;
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable) //���� ������� �� �������� ���������� �������� ������ � ����� ������������� 
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor); //��������� ���������� ������
                frontier.Enqueue(neighbor); // �������� � ����� ������� �������
            }
        }
    }

    IEnumerator BreadthFirstSearch() //�������� ������ ����
    {
        bool isRunning = true;
        frontier.Enqueue(startNode); //�������� ��������� ���� � ����� �������
        reached.Add(startCoords, startNode); //�������� ���������� � ��������� ���� � ������ ������������� �����

        while(frontier.Count > 0 && isRunning) // ���� ����� � ������ ������ 0
        {
            currentSearchNode = frontier.Dequeue(); //������ ������� ���� �� ������� � �������� ������� ���� ������ � �������
            currentSearchNode.isExplored = true; //�������� ������� ���� ��� ����������
            
            ExploreNeighbours();
            if (currentSearchNode.coordinates == endCoords) // ���� ���������� �������� ���� ����� ��������� ����
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
