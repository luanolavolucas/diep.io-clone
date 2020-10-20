using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsMouse : MonoBehaviour
{
    public GameObject pivot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        //transform.rotation.SetLookRotation(difference, Vector3.forward);
        //transform.LookAt(target, Vector3.back);
        //Vector3.fo
        
        Vector3 difference = target - transform.position;
        //Vector3 difference = transform.position - target;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        //float angle = Vector2.Angle(transform.right,  new Vector2(difference.x,difference.y));
        //transform.RotateAround(pivot.transform.position, Vector3.back, angle);
    }
}
