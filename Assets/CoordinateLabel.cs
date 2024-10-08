using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class CoordinateLabel : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    float snapMoveX;
    float snapMoveY;


    void Awake()
    {
        snapMoveX = UnityEditor.EditorSnapSettings.move.x; //var for dividing x grid position
        snapMoveY = UnityEditor.EditorSnapSettings.move.z; //var for dividing z grid position
        label = GetComponent<TextMeshPro>();
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
}
