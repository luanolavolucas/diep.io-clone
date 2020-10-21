using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{
    protected abstract void ApplyPowerUp(Ship s);

    void OnTriggerEnter2D(Collider2D collision)
    {
        Ship s = collision.GetComponent<Ship>();
        if(s != null)
        {
            ApplyPowerUp(s);
            Destroy(this.gameObject);
        }
    }
}
