using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Tile> path = new List<Tile>(); // list of path where the enemy can move
    [SerializeField] [Range(0f, 5f)] private float enemySpeed = 1f; //enemy speed
    private bool isDefaultSpeed = true;
    Enemy enemy;

    // Start is called before the first frame update
    private void OnEnable()
    {
        FindPath();
        ReturnToStartPosition();
        IncreaseSpeed();
        StartCoroutine(PrintWaypointName());  
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void FindPath() // adding the path tiles form the hierarchy
    {
        path.Clear(); // delete current path to prevent copying
        GameObject parent = GameObject.FindGameObjectWithTag("Path"); //find the parent GameObject with "Path" tag

        foreach (Transform child in parent.transform) //adding child elements of parent object to the list based on tag
        {
            Tile waypoint = child.GetComponent<Tile>();
            if (waypoint != null) 
            {
                path.Add(waypoint);

            }
        }

        /*
        GameObject[] waypoints =  GameObject.FindGameObjectsWithTag("Path");
         
        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
        
        path.Reverse(); // invert elements of the list
        */
    }

    public void ReturnToStartPosition() //begin path from the beginning
    {
        transform.position = path[0].transform.position;
    }

    public void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealMoney();
    }

    public void IncreaseSpeed()
    {
        if (enemySpeed == 1f && isDefaultSpeed)
        {
            isDefaultSpeed = false;
            return;
        }
        enemySpeed += 0.05f;
    }

    public IEnumerator PrintWaypointName()
    {
        foreach (Tile waypoint in path) 
        {
            Vector3 startPosition = transform.position; //start position of the current tile
            Vector3 endPosition = waypoint.transform.position; //start position of the next tile
            Vector3 directionTarget = endPosition - startPosition;
            Quaternion startRotation = transform.rotation; //current rotation
            Quaternion endRotation = Quaternion.LookRotation(directionTarget); //rotate the object to the point
            float timePercent = 0f;


            while (timePercent <= 1f)
            {

                /*if (timePercent < 0.25f)
                {
                    float rotationAngle = 4 * rotation * Time.deltaTime * enemySpeed;
                    transform.Rotate(new Vector3(0, rotationAngle, 0));
                }*/

                timePercent += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, timePercent); //smooth movement from start to end position
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, 5 * timePercent); //smooth rotation from start to end position
                yield return new WaitForEndOfFrame(); //stops the current frame to make smooth animation

                
            }
            
        }
        FinishPath();
    }
}
