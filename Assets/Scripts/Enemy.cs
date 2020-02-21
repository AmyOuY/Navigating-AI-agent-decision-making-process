using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    float speed = 10f;
    Collider mCollider;
    public LayerMask m_LayerMask;
    Vector3 forward = new Vector3(0, 0, 0);
	
	// Update is called once per frame
	void FixedUpdate () {
        //move enemy according to designed path
        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        currentPosition.z += Time.deltaTime * speed;
        transform.position = currentPosition;   

        if (speed > 0)
        {
            forward = new Vector3(0, 0, 0.5f);
        }
        else
        {
            forward = new Vector3(0, 0, -0.5f);
        }

        //kill agent if it is inside the enemy's sightview 
        Collider[] hitColliders = Physics.OverlapBox(transform.position + forward, new Vector3(4.5f, 0.5f, 2f), Quaternion.identity, m_LayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Agent")
            {
                Destroy(hitColliders[i].gameObject);
            }

            i++;
        }

        //disable collider of middle obstacle when enemy passing through and enable it after
        Collider colliderGC = gameObject.GetComponent<Collider>();
        if (colliderGC.enabled == false) {
            if (transform.position.z > -1.5f && transform.position.z < 1.5f)
            {
                colliderGC.enabled = false;
            }
            else
            {
                colliderGC.enabled = true;
            }
        }
    }


    //when encountered middle obstacle, enemies either go thorough obstacle, disappear and respawn or reverse direction
    public void OnCollisionEnter(Collision other)
    {
        Collider colliderGC = gameObject.GetComponent<Collider>();
        //respawn enemy if reaching door
        if (other.gameObject.tag == "door")
        {
            Destroy(gameObject);
        }
        //3 possibilities when enemy hitting middle obstacle
        if (other.gameObject.tag == "obstacle")
        {
            int r = Random.Range(0, 3);
            //enemy disappear
            if (r == 0)
            {
                Destroy(gameObject);
            }
            //enemy go through middle obstacle unhindered
            else if (r == 1)
            {
                colliderGC.enabled = false;               
            }
            //enemy turn direction
            else
            {
                speed = -speed;
            }
        }
    }


    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position + forward, new Vector3(9f, 1f, 4f));
    //}


}
