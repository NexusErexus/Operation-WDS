using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.green;
    [SerializeField] private Color blockedColor = Color.red;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(0, 0, 255);
    //[SerializeField] private Color pathColor = Color.black;
    [SerializeField] private InputActionReference toggleLabelAction; //reference to action map

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    

    /*private float snapMoveX;
    private float snapMoveY;*/


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
        UpdateObjectName();
    }

    private void Start()
    {
        label.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Application.isPlaying) //edit mode
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        SetLabelColor();
    }
    private void OnEnable() 
    {
        toggleLabelAction.action.Enable(); //enable action map
        toggleLabelAction.action.performed += OnToggleLabelVisible;
    }
    private void OnDisable()
    {
        toggleLabelAction.action.Disable(); //disable avtion map
        toggleLabelAction.action.performed -= OnToggleLabelVisible;
    }
    public void DisplayCoordinates() //show coordinates of tile
    {
        if (gridManager == null) { return; }
        #if UNITY_EDITOR
        //snapMoveX = gridManager.UnityGridSize; //var for dividing x grid position
        //snapMoveY = grid; //var for dividing z grid position
        //Debug.Log((snapMoveX, snapMoveY));
        #else
        snapMoveX = 1f;
        snapMoveY = 1f;
        #endif

        label.enabled = true;
        coordinates.x = (int)(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = (int)(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = ($"{coordinates.x}, {coordinates.y}"); //label name for example = "1,1";
    }

    public void UpdateObjectName() //update label text while moving it
    {
        transform.parent.name = ($"[{label.text}]");
    }

    public void SetLabelColor() //changing color of label
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return; }
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    public void OnToggleLabelVisible(InputAction.CallbackContext ctx) //turn on/off label visibillity (Default: C)
    {
        label.enabled = !label.IsActive();
    }
    public void Test()
    {

    }
}
