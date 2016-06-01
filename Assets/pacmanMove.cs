using UnityEngine;
using System.Collections;

public class pacmanMove : MonoBehaviour
{
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
	    dest = transform.position;

	}
	
	// Update is called once per frame
	void FixedU1pdate ()
	{
	    Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p); 

	}

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());

    }
}
