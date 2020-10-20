using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour, IDamageable
{
    public ShipData shipData;
    public float Health {get; protected set;}

    // Start is called before the first frame update
    void Start()
    {
        Health = shipData.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Move(float directionX, float directionY)
    {
        Vector3 moveAmount = new Vector3(directionX, directionY, 0) * shipData.speed * Time.deltaTime;
        transform.Translate(moveAmount,Space.World);
    }

    public void Damage()
    {

    }
}
