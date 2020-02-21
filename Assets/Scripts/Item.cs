using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public GameObject item;
    public static List<GameObject> items = new List<GameObject>();
   

    // Use this for initialization
    void Start () {
        ItemDeploy();
    }

    
    //deploy one item to each alcove
    public void ItemDeploy()
    {            
        for (int i = 0; i < 5; i++)
        {
            Vector3 position1 = new Vector3(-12.0f, 0.5f, -20.0f + 10.0f * i);
            GameObject item1 = Instantiate(item, position1, Quaternion.identity);
            items.Add(item1);
            Vector3 position2 = new Vector3(12.0f, 0.5f, -20.0f + 10.0f * i);
            GameObject item2 = Instantiate(item, position2, Quaternion.identity);
            items.Add(item2);
        }
    }



   


}
