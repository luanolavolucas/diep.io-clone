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
    }

    void FixedUpdate()
    {
        ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

}
