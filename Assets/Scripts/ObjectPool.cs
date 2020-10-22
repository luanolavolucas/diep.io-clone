using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ObjectPool:MonoBehaviour
{
    private GameObject prefab;
    private List<GameObject> pool;
    private  bool canBeDestroyed;

    public void Init(GameObject prefab, int poolSize)
    {
        this.prefab = prefab;
        pool = new List<GameObject>(poolSize);

        gameObject.name = prefab.name + " Pool";
        transform.parent = GameManager.Instance.bulletPools.transform;

        for (int i = 0; i < poolSize; i++)
        {
            NewPooledObject();
        }
    }

    private GameObject NewPooledObject()
    {
        GameObject instance = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        instance.transform.parent = transform;
        pool.Add(instance);
        instance.SetActive(false);
        return instance;
    }

    internal void SetDestructionFlag(bool v)
    {
        canBeDestroyed = true;
    }

    private void Update()
    {
        //If all objects are inactive and the flag has been set, destroy the pool.
        if (pool.All(go => !go.activeInHierarchy) && canBeDestroyed)
            Destroy(gameObject);
    }

    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        //Grabs the first available object for instantiating
        GameObject instance = pool.FirstOrDefault(go => !go.activeInHierarchy);

        //If all objects are in use, expand the size of the pool.
        if (instance == null)
            instance = NewPooledObject();

        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.SetActive(true);
        return instance;
    }
}
