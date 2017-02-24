using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostAIMove : MonoBehaviour
{
    public Transform startWayPoint;
    private bool reachedWayPoint = false;
    public Transform[] waypoints;
    public float speed = 0.3f;
    private Transform currentWayPoint;
    public GameObject player;

    public LayerMask layer;

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
            Vector2 pos = transform.position;
            List<Transform> points = new List<Transform>();
            RaycastHit2D hit;
            float shortestDistance = 600;
            bool playerFound = false;
            		
            hit = Physics2D.Linecast(pos+Vector2.up, pos+Vector2.up * 100, layer);
            waypointhit(hit, ref points, ref playerFound);

            hit = Physics2D.Linecast(pos+Vector2.down, pos+Vector2.down * 100, layer);
            waypointhit(hit, ref points, ref playerFound);
            
            hit = Physics2D.Linecast(pos+Vector2.left, pos+Vector2.left * 100, layer);
            waypointhit(hit, ref points, ref playerFound);

            hit = Physics2D.Linecast(pos+Vector2.right, pos+Vector2.right * 100, layer);
            waypointhit(hit, ref points, ref playerFound);

            foreach (Transform point in points)
            {
                float dist = Vector2.Distance(point.position, player.transform.position);
                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    currentWayPoint = point;
                }
                    
            }
        }

        Vector2 dir = currentWayPoint.position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }

    void waypointhit(RaycastHit2D hit, ref List<Transform> list, ref bool playerFound)
    {

        if (hit && !playerFound && hit.collider.gameObject.tag == "Waypoint")
        {
            list.Add(hit.transform);
        }

    }
		  

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
            Destroy(co.gameObject);
    }
}
