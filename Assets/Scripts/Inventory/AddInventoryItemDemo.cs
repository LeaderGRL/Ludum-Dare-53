using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventoryItemDemo : MonoBehaviour
{
    public Sprite imageNone;
    
    private void Start()
    {
        for (var i = 0; i < 15; i++)
        {
            var item = ScriptableObject.CreateInstance<Resource>();
            item.id = i;
            item.type = "None";
            item.icon = imageNone;

            InventoryManager.instance.Add(item);
        }
    }
}

