using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text displayInfo;
    public Canvas canvas;
    GameObject agent;
    GameObject agentAI;
    

    // Use this for initialization
    void Start () {
        canvas.enabled = false;
        agent = GameObject.Find("agent1");
        agentAI = GameObject.Find("agentai");
    }


    //simulation ends if all items acuired or both agents captured
    void Update()
    {
        if ((agent == null && agentAI == null) || Item.items.Count == 0)
        {
            canvas.enabled = true;
            UpdateUI();
        }        
        
    }


    //Declare winner/tie based on number of items acquired
    public void UpdateUI()
    {      
        if (Agent.currItems > AgentAI.currItemsAI)
        {
            displayInfo.text = "Game Over. Agent Win!";
        }
        else if (Agent.currItems < AgentAI.currItemsAI)
        {
            displayInfo.text = "Game Over. AIAgent Win!";
        }
        else
        {
            displayInfo.text = "Game Over. Tie!";
        }
    }
    
	
}
