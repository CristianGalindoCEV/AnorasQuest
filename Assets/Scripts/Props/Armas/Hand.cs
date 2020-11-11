using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Camera cam;
    public float maximumlenght;

    private Ray raymouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            raymouse = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(raymouse.origin, raymouse.direction, out hit, maximumlenght))
            {
                RotateToMouseDirection(gameObject, hit.point);
            }else
            {
                var pos = raymouse.GetPoint(maximumlenght);
                RotateToMouseDirection(gameObject, hit.point);
            }
        }
    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}
