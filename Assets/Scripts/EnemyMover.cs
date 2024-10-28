using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float enemySpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        FindPath();
        //ReturnToStartPosition();
        StartCoroutine(PrintWaypointName());  
    }

    void FindPath()
    {
        path.Clear();
        GameObject[] waypoints =  GameObject.FindGameObjectsWithTag("Path");

        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
            
    }

    void ReturnToStartPosition()
    {
        transform.position = path[7].transform.position;
        Debug.Log(path[0]);
    }

    IEnumerator PrintWaypointName()
    {

        foreach (var waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float timePercent = 0f;

            Vector3 directionTarget = endPosition - startPosition;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(directionTarget);

            while (timePercent <= 1f)
            {

                /*if (timePercent < 0.25f)
                {
                    float rotationAngle = 4 * rotation * Time.deltaTime * enemySpeed;
                    transform.Rotate(new Vector3(0, rotationAngle, 0));
                }*/

                timePercent += Time.deltaTime * enemySpeed;
                //Debug.Log(timePercent);
                transform.SetPositionAndRotation(Vector3.Lerp(startPosition, endPosition, timePercent), Quaternion.Slerp(startRotation, endRotation, 5 * timePercent));
                yield return new WaitForEndOfFrame();
            }


        }

    }
}
