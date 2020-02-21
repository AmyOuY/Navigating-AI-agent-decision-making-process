using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {

    public GameManager gameManager;
    public Rigidbody rb;   
    public static int currItems = 0;
    GameObject enemy1;
    GameObject enemy2;
    GameObject agentAI;

    public float Speed = 5f;
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private int teleport = 2;


    void Start()
    {
        _body = GetComponent<Rigidbody>();
        enemy1 = GameObject.Find("enemy1");
        enemy2 = GameObject.Find("enemy2");
        agentAI = GameObject.Find("agentai");
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    private void Update()
    {
        //use "arrrow" keys to control movement of agent
        _inputs = Vector3.zero;
        _inputs.z = -Input.GetAxis("Horizontal");
        _inputs.x = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero) {
            transform.forward = _inputs;
        }
        enemy1 = GameObject.Find("enemy1");
        enemy2 = GameObject.Find("enemy2");
        agentAI = GameObject.Find("agentai");

        //agent apply teleport function when player press down "space" key
        //will respawn nearest enemy or move agentAI to other location 
        if (Input.GetKeyDown("space") && teleport > 0)
        {
            //find nearest enemy or agentAI and move agentAI to new location or respawn enemy
            float dist1 = float.MaxValue;
            float dist2 = float.MaxValue;
            float dist3 = float.MaxValue;
            if (enemy1 != null)
            {              
                dist1 = Vector3.Distance(transform.position, enemy1.transform.position);
            }
            if (enemy2 != null)
            {               
                dist2 = Vector3.Distance(transform.position, enemy2.transform.position);
            }
            if (agentAI != null)
            {             
                dist3 = Vector3.Distance(transform.position, agentAI.transform.position);
            }
            float minDist = 1000000f;
            int idx = -1;
            if (dist1 < dist2)
            {
                idx = 1;
                minDist = dist1;
            }
            else
            {
                idx = 2;
                minDist = dist2;
            }
            
            if (minDist < dist3)
            {
                if (idx == 1)
                {
                    Destroy(enemy1);
                }
                else
                {
                    Destroy(enemy2);
                }
            }
            else
            {
                Vector3 position = new Vector3(0, 0, 0);
                int r = Random.Range(0, 5);
                int t = Random.Range(0, 2);
                if (t == 0)
                {
                    position = new Vector3(-13.0f, 1f, -22.0f + 10.0f * r);
                }
                else if (t == 1)
                {
                    position = new Vector3(13.0f, 1f, -22.0f + 10.0f * r);
                }
                if (agentAI != null)
                {
                    NavMeshAgent agentai = agentAI.GetComponent<NavMeshAgent>();
                    agentai.enabled = false;
                    agentAI.transform.position = position;
                    agentai.enabled = true;
                }

            }
            teleport--;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);      

    }


    //agent collect item in alcove
    public void OnCollisionEnter(Collision other)
    {       
        if (other.gameObject.tag == "item")
        {         
            Destroy(other.gameObject);
            currItems++;           
            Item.items.Remove(other.gameObject);          
        }
    }

    

}
