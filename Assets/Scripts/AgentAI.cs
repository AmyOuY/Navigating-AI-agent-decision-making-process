using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour {

    public GameManager gameManager;
    NavMeshAgent agentAI;
    GameObject enemy1;
    GameObject enemy2;
    GameObject agent;

    public float speed = 5f;
    public static int currItemsAI = 0;
    private int teleport = 2;



    // Use this for initialization
    void Start () {
        agentAI = GetComponent<NavMeshAgent>();
        enemy1 = GameObject.Find("enemy1");
        enemy2 = GameObject.Find("enemy2");
        agent = GameObject.Find("agent1");
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {

        //AIagent apply teleport function when the total number of his collected items is fewer than agent's
        //will respawn nearest enemy or move nearest agent to other location  
        enemy1 = GameObject.Find("enemy1");
        enemy2 = GameObject.Find("enemy2");
        agent = GameObject.Find("agent1");

        //AIagent always tries to collect the nearest available item 
        GameObject nearestItem = NearestItem();
        if (nearestItem != null)
        {
            agentAI.SetDestination(nearestItem.transform.position);
        }
        
        //apply teleport function when AIagent has fewer items than agent
        if ((currItemsAI < Agent.currItems) && teleport > 0)
        {
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
            if (agent != null)
            {              
                dist3 = Vector3.Distance(transform.position, agent.transform.position);
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
                if (agent != null)
                {
                    agent.transform.position = position;

                }

            }
            teleport--;
        }

    }


    //AIagent collect item in each alcove
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "item")
        {
            
            Destroy(other.gameObject);
            currItemsAI++;
           
            Item.items.Remove(other.gameObject);
            //Debug.Log(Item.items.Count);
        }
    }


    //function for AIagent to find the nearest item
    public GameObject NearestItem()
    {       
        if (Item.items.Count > 0)
        {
            float mindistance = 10000f;
            int index = -1;
           
            for (int i = 0; i < Item.items.Count; i++)
            {
                if (Item.items[i] != null)
                {
                    float distToItem = Vector3.Distance(transform.position, Item.items[i].transform.position);
                    if (distToItem < mindistance)
                    {
                        mindistance = distToItem;
                        index = i;
                    }
                }
            }

            return Item.items[index];
        }
        return null;
    }
    
}
