using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public InputActionReference mouseClickAction;
    //CoordinateLabel coordinateLabel = new CoordinateLabel();

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
                Debug.Log(transform.name);
            }
        }
        
    }
}
