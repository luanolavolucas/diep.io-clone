using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : ShipController
{

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ship.Fire();
        }
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        ship.Aim(target);
    }

    void FixedUpdate()
    {
        ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}
