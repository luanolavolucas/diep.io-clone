using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawnPoint : MonoBehaviour
{
    public bool CanSpawn
    {
        get
        {
            //TODO: Add some checks to this.
            return true;
        }
    }

    public bool playerSpawnPoint;
}
