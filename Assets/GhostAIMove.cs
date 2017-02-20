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
            int mask = LayerMask.NameToLayer("Waypoint");
            int res = 1 << mask;

            hit = Physics2D.Linecast(pos, Vector2.up * 100, layer);
            waypointhit(hit, ref points);

            hit = Physics2D.Linecast(pos, Vector2.down * 100, layer);
            waypointhit(hit, ref points);

            Debug.DrawRay(pos, Vector2.left * 100);
            hit = Physics2D.Linecast(pos, Vector2.left * 100, layer);
            waypointhit(hit, ref points);

            hit = Physics2D.Linecast(pos, Vector2.right * 100, layer);
            waypointhit(hit, ref points);
            Debug.Log("bam");







            //  cur = (cur + 1) % waypoints.Length;
        }

        Vector2 dir = currentWayPoint.position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

    }

    void waypointhit(RaycastHit2D hit, ref List<Transform> list)
    {
        if (hit && hit.collider.gameObject.tag == "Waypoint")
        {
            list.Add(hit.transform);
        }

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
