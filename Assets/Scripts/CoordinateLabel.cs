using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;
    public InputActionReference toggleLabelAction;

    float snapMoveX;
    float snapMoveY;


    void Awake()
    {
        snapMoveX = UnityEditor.EditorSnapSettings.move.x; //var for dividing x grid position
        snapMoveY = UnityEditor.EditorSnapSettings.move.z; //var for dividing z grid position
        waypoint = GetComponentInParent<Waypoint>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
        UpdateObjectName();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        ColorCoordinates();
    }
    private void OnEnable()
    {
        toggleLabelAction.action.performed += OnToggleLabels;
    }
    private void OnDisable()
    {
        toggleLabelAction.action.performed -= OnToggleLabels;
    }
    void DisplayCoordinates()
    {
        //label.text = "1,1";

        coordinates.x = (int)(transform.parent.position.x / snapMoveX);
        coordinates.y = (int)(transform.parent.position.z / snapMoveY);
        label.text = ($"{coordinates.x}, {coordinates.y}");
    }

    void UpdateObjectName()
    {
        transform.parent.name = ($"[{label.text}]");
    }

    void ColorCoordinates()
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

    void OnToggleLabels(InputAction.CallbackContext ctx)
    {
        Debug.Log("hi");
        label.enabled = !label.IsActive();
    }
}
