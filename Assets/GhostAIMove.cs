using UnityEngine;
using System.Collections;

public class GhostAIMove : MonoBehaviour
{
    public Transform startWayPoint;
    private bool reachedWayPoint = false;
    public Transform[] waypoints;
    public float speed = 0.3f;
    private Transform currentWayPoint;

    void Start()
    {
        currentWayPoint = startWayPoint;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //move to position
        if (transform.position != currentWayPoint.position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, currentWayPoint.position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);

        }

        //find a new waypoint
        else
        {
            foreach (Transform waypoint in waypoints)
            {

                
            }
          //  cur = (cur + 1) % waypoints.Length;
        }

        Vector2 dir = currentWayPoint.position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }

    bool valid(Vector2 dir)
    {
        ArrayList validPositions = new ArrayList();
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
            Destroy(co.gameObject);
    }
}
