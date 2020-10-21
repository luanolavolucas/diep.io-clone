using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    [HideInInspector]
    public Collider2D[] bulletDespawners;


    // Start is called before the first frame update
    void Start()
    {
        bulletDespawners = GetComponentsInChildren<Collider2D>();
        if (bulletDespawners.Count() == 0)
            Debug.LogWarning("Warning: No bullet despawners found.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
