using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
[RequireComponent(typeof(Ship))]
public class ShipController : MonoBehaviourPunCallbacks
{
    public Ship ship;
    protected virtual void Awake()
    {
        ship = GetComponent<Ship>();
    }
}
