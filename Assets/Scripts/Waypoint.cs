using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isNotPathPlaceable;
    [SerializeField] bool isTilePlaceable;
    public bool IsTilePlaceable { get { return isTilePlaceable; } }
    [SerializeField] GameObject turretPrefab;
    GameObject destroyTurret;
    public InputActionReference mouseClickAction;

    private void OnEnable()
    {
        mouseClickAction.action.performed += OnMouseClickAction;
    }

    private void OnDisable()
    {
        mouseClickAction.action.performed -= OnMouseClickAction;
    }

    public void OnMouseClickAction(InputAction.CallbackContext ctx)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && isTilePlaceable)
            {
                Vector3 correctPosition = transform.position + new Vector3(0, 0.25f, 0);
                destroyTurret = Instantiate(turretPrefab, correctPosition, Quaternion.identity);
                isTilePlaceable = false;
            }
            else if (hit.transform == transform && !isTilePlaceable && !isNotPathPlaceable)
            {
                Destroy(destroyTurret);
                isTilePlaceable = true;
            }

        }

    }
}
