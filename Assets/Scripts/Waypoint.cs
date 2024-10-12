using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
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

    public void OnMouseClickAction(InputAction.CallbackContext cxt)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform && isPlaceable)
            {
                Vector3 correctPosition = transform.position + new Vector3(0, 0.25f, 0);
                destroyTurret = Instantiate(turretPrefab, correctPosition, Quaternion.identity);
                isPlaceable = false;
            }
            else if (hit.transform == transform && !isPlaceable)
            {
                Destroy(destroyTurret);
                isPlaceable = true;
            }
            
        }
        
    }
}
