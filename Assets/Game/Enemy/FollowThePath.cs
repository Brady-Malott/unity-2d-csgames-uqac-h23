using UnityEngine;
using System.Collections.Generic;

public class FollowThePath : MonoBehaviour
{
    public static List<FollowThePath> allEnemies = new List<FollowThePath>();

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 2f;
    private int waypointIndex = 0;

    public Transform target;
    private float maxDistance = 3f;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        transform.position = waypoints[waypointIndex].transform.position;

        allEnemies.Add(this);
    }

    private void Update()
    {
        bool playerInRange = false;

        foreach (var enemy in allEnemies)
        {
            if (Vector2.Distance(enemy.transform.position, target.transform.position) < maxDistance)
            {
                Debug.Log(enemy + " is in range! (" + Vector2.Distance(enemy.transform.position, target.transform.position) + " < " + maxDistance + ")");
                playerInRange = true;
                break;
            }
        }

        if (Vector2.Distance(transform.position, target.position) < maxDistance || playerInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,
           waypoints[waypointIndex].transform.position,
           moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex++;

            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
}