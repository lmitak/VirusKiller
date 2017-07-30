using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    //public PooledObject[] pooledObjects;
    public static ObjectPool instance;
    public GameObject pooledObject;
    public int pool = 0;
    private List<GameObject> objects;

	// Use this for initialization
	void Start () {
        instance = this;
        objects = new List<GameObject>();

        // if pool is set, instantiate that amount of objects
        if(this.pool != 0 && this.pool != -1)
        {
            this.InstantiatePooledObjects(pooledObject, pool);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Instantiate certain amount of objects
    /// </summary>
    /// <param name="pooledObject">Object that will be instantiated</param>
    /// <param name="amount">Amount that needs to be instantiated</param>
    public void InstantiatePooledObjects(GameObject pooledObject, int amount)
    {
        this.pooledObject = pooledObject;
        while(amount-- > 0)
        {
            GameObject obj = Instantiate<GameObject>(pooledObject);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }

    /// <summary>
    /// Returns first available pooled object
    /// </summary>
    /// <returns>Reference to a game object</returns>
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            if(!objects[i].activeInHierarchy)
            {
                return objects[i];
            }
        }
        /// if there are no free objects, instantiate and return a new instance
        GameObject obj = Instantiate<GameObject>(this.pooledObject);
        objects.Add(obj);
        return obj;
    }
    
}


