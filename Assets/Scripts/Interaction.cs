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

            if (Input.GetMouseButtonDown(0))
            {
                
                // AFFICHER L'UI
            }
        }
        else
        {
            if (interactableObject != null)
            {
                interactableObject.GetComponent<Outline>().enabled = false;
                interactableObject = null;
            }
            
        }
    }

    private void DisplayPlanetUI(string planetName)
    {
        
    }
}

