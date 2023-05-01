using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventoryItemDemo : MonoBehaviour
{
    public Resource item;
    public Resource item2;
    public Resource item3;
    // Start is called before the first frame update
    private void Start()
    {
        //for (var i = 0; i < 15; i++)
        //{
        //    var item = ScriptableObject.CreateInstance<Resource>();
        //    item.id = i;
        //    item.type = "None";
        //    item.icon = imageNone;

        //    InventoryManager.instance.Add(item);
        //}
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            InventoryManager.instance.Add(item);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            InventoryManager.instance.Add(item2);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            InventoryManager.instance.Add(item3);
        }
    }
}

