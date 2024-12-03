using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.green;
    [SerializeField] private Color blockedColor = Color.red;
    [SerializeField] private InputActionReference toggleLabelAction; //reference to action map

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private Waypoint waypoint;
    

    private float snapMoveX;
    private float snapMoveY;


    private void Awake()
    {
        waypoint = GetComponentInParent<Waypoint>();
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
        #if UNITY_EDITOR
        snapMoveX = UnityEditor.EditorSnapSettings.move.x; //var for dividing x grid position
        snapMoveY = UnityEditor.EditorSnapSettings.move.z; //var for dividing z grid position
        #else
        snapMoveX = 1f;
        snapMoveY = 1f;
        #endif

        label.enabled = true;
        coordinates.x = (int)(transform.parent.position.x / snapMoveX);
        coordinates.y = (int)(transform.parent.position.z / snapMoveY);
        label.text = ($"{coordinates.x}, {coordinates.y}"); //label name for example = "1,1";
    }

    public void UpdateObjectName() //update label text while moving it
    {
        transform.parent.name = ($"[{label.text}]");
    }

    public void SetLabelColor() //changing color of label
    {
        if (waypoint.IsTilePlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    public void OnToggleLabelVisible(InputAction.CallbackContext ctx) //turn on/off label visibillity (Default: C)
    {
        label.enabled = !label.IsActive();
    }
}
