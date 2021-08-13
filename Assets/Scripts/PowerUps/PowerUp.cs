using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{
    protected abstract void ApplyPowerUp(Character s);

    void OnTriggerEnter2D(Collider2D collision)
    {
        Character s = collision.GetComponent<Character>();
        if(s != null)
        {
            ApplyPowerUp(s);
            Destroy(this.gameObject);
        }
    }
}
