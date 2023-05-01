using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventoryItemDemo : MonoBehaviour
{
    public Resource item;
    public Resource item2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            InventoryManager.instance.Add(item);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            InventoryManager.instance.Add(item2);
        }
    }
}
