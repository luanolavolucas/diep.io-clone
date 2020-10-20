using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ship))]
public class ShipController : MonoBehaviour
{
    public Ship ship;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponentInChildren<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
