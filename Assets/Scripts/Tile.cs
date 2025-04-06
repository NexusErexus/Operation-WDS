using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isRoadPlaceable; // road path check (false)
    [SerializeField] bool isTilePlaceable; //check for placing turret
    [SerializeField] private Tower turretPrefab;
    [SerializeField] private InputActionReference mouseClickAction; //reference to action map
    public bool IsTilePlaceable { get { return isTilePlaceable; } }
    
    private GameObject turretObject;
    
    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isTilePlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnEnable()
    {
        mouseClickAction.action.Enable(); //enable action map
        mouseClickAction.action.performed += OnMouseClickAction;
    }

    private void OnDisable()
    {
        mouseClickAction.action.Disable(); //disable action map
        mouseClickAction.action.performed -= OnMouseClickAction;
    }

    public void OnMouseClickAction(InputAction.CallbackContext ctx) 
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue(); //read the current position of mouse when you click

        Ray ray = Camera.main.ScreenPointToRay(mousePosition); //create a ray for reading value from the camera
        RaycastHit hit; //var for reading value from the ray
        if (Physics.Raycast(ray, out hit)) //checking raycast (collider required)
        {
            if (hit.transform == transform && isTilePlaceable) //ray coordinates equals tile coordinates
            {
                Vector3 correctPosition = transform.position + new Vector3(0, 0.25f, 0); //create position for instantiating turret
                //turretObject = Instantiate(turretPrefab, correctPosition, Quaternion.identity); //spawn the turret
                bool isPlaced = turretPrefab.CreateTower(turretPrefab, correctPosition);
                isTilePlaceable = !isPlaced;
            }
            /*else if (hit.transform == transform && !isTilePlaceable && !isNotPathPlaceable) //destroy the object when tile is busy by another turret
            {
                Destroy(turretObject);
                isTilePlaceable = true;
            }*/

        }
    }
}
