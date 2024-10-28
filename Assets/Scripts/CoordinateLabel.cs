using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.grey;
    [SerializeField] private InputActionReference toggleLabelAction; //reference to action map

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();
    private Waypoint waypoint;
    

    private float snapMoveX;
    private float snapMoveY;


    private void Awake()
    {

        snapMoveX = UnityEditor.EditorSnapSettings.move.x; //var for dividing x grid position
        snapMoveY = UnityEditor.EditorSnapSettings.move.z; //var for dividing z grid position
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
        ColorCoordinates();
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
        //label name for example = "1,1";
        label.enabled = true;
        coordinates.x = (int)(transform.parent.position.x / snapMoveX);
        coordinates.y = (int)(transform.parent.position.z / snapMoveY);
        label.text = ($"{coordinates.x}, {coordinates.y}");
    }

    public void UpdateObjectName() //update label text while moving it
    {
        transform.parent.name = ($"[{label.text}]");
    }

    public void ColorCoordinates() //changing color of label
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

    public void OnToggleLabelVisible(InputAction.CallbackContext ctx) //turn on/off label visibillity
    {
        label.enabled = !label.IsActive();
    }
}
