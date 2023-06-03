using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepItem : MonoBehaviour
{

    [SerializeField] float sweepForce;

    private Color HighlightColor = Color.red;
    private Color initialColor;

    private Collider hitObject;
    private Material hitObjectMaterial;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Item"))
        {
            if (hitObject != null)
                hitObjectMaterial.color = initialColor;
            hitObject = hit.collider;
            hitObjectMaterial = hitObject.GetComponent<Renderer>().material;
            hitObjectMaterial.color = HighlightColor;
            Debug.Log("raycast hit");
            
            if (Input.GetMouseButtonDown(0))
            {
                hitObject.GetComponent<Rigidbody>().AddForce(0, 0, sweepForce);
            }

            if (Input.GetMouseButtonDown(1))
            {
                hitObject.GetComponent<Rigidbody>().AddForce(0, 0, -sweepForce);
            }
        }
        else if (hitObject != null)
        {
            hitObjectMaterial.color = initialColor;
            hitObject = null;
            hitObjectMaterial = null;
            Debug.Log("left raycast");
        }
    }
}
