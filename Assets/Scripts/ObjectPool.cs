using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ObjectPool 
{
    private GameObject prefab;
    private GameObject poolGO;
    private List<GameObject> pool;

    public ObjectPool(GameObject prefab, int poolSize)
    {
        this.prefab = prefab;
        pool = new List<GameObject>(poolSize);

        poolGO = new GameObject(prefab.name + " Pool");
        poolGO.transform.parent = GameManager.Instance.bulletPools.transform;

        for (int i = 0; i < poolSize; i++)
        {
            NewPooledObject();
        }
    }

    private GameObject NewPooledObject()
    {
        GameObject instance = GameObject.Instantiate(prefab, poolGO.transform.position, Quaternion.identity);
        instance.transform.parent = poolGO.transform;
        pool.Add(instance);
        instance.SetActive(false);
        return instance;
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
