using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject interactableObject;
    public Camera cam;
    Ray ray;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Interactable")
            {
                interactableObject = hit.transform.gameObject;
                interactableObject.GetComponent<Outline>().enabled = true;
            }
        }
        else
        {
            interactableObject.GetComponent<Outline>().enabled = false;
            interactableObject = null;
        }
    }
}

