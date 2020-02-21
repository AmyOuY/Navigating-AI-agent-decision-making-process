using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject agent;
    public GameObject agent1;
    public GameObject agentAI;
    public GameObject agentai;
    public GameObject Enemy;
    public GameObject enemy1;
    public GameObject enemy2;

    Vector3 start1 = new Vector3(6f, 1f, -28f);
    Vector3 start2 = new Vector3(-6f, 1f, -28f);
    

    // Use this for initialization
    void Start () {       
        agent1 = DeployAgent(false);
        agent1.name = "agent1";
        agentai = DeployAgent(true);
        agentai.name = "agentai";
        enemy1 = Instantiate(Enemy, start1, Quaternion.identity) as GameObject;
        enemy1.name = "enemy1";
        enemy2 = Instantiate(Enemy, start2, Quaternion.identity) as GameObject;
        enemy2.name = "enemy2";
    }



    //respawn enemy to ensure always two enemies on the level
    void Update()
    {
        if (enemy1 == null)
        {
            enemy1 = Instantiate(Enemy, start1, Quaternion.identity);
            enemy1.name = "enemy1";
        }
        if (enemy2 == null)
        {
            enemy2 = Instantiate(Enemy, start2, Quaternion.identity);
            enemy2.name = "enemy2";
        }     
    }




    //spawn agent and agentAI into different alcoves
    public GameObject DeployAgent(bool AI)
    {
        Vector3 position = new Vector3(0, 0, 0);
        int r = Random.Range(0, 5);
        int t = Random.Range(0, 2);

        //based on random number t to select random alcove in same row
        if (t == 0)
        {
            position = new Vector3(-13.0f, 1f, -22.0f + 10.0f * r);
        }
        else if (t == 1)
        {
            position = new Vector3(13.0f, 1f, -22.0f + 10.0f * r);
        }

        //spawn agent/AIagent into different alcoves
        GameObject deployedAgent;
        Vector3 temp = new Vector3(0, 0, 0);
        if (AI)
        {
            if (agent1 != null && agent1.transform.position != position)
            {
                deployedAgent = Instantiate(agentAI, position, Quaternion.identity);
            }
            else
            {
                temp = position;
                temp.x = -temp.x;
                position = temp;
                deployedAgent = Instantiate(agentAI, position, Quaternion.identity);
            }
        }
        else
        {
            if (agentAI != null && agentAI.transform.position != position)
            {
                deployedAgent = Instantiate(agent, position, Quaternion.identity);
            }
            else
            {
                temp = position;
                temp.x = -temp.x;
                position = temp;
                deployedAgent = Instantiate(agent, position, Quaternion.identity);
            }
        }
        return deployedAgent;
    }
    
}
