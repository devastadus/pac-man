using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour
{

    public Transform[] waypoints;
    private int cur = 0;
    public float speed = 0.3f;
	// Update is called once per frame
	void FixedUpdate () {

	    if (transform.position != waypoints[cur].position)
	    {
	        Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);

	    }
	    else
	    {
	        cur = (cur + 1)%waypoints.Length;
	    }

	    Vector2 dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX",dir.x);
        GetComponent<Animator>().SetFloat("DirY",dir.y);

    }
}
