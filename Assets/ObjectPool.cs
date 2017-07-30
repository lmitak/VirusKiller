using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    //public PooledObject[] pooledObjects;
    public static ObjectPool instance;
    public GameObject pooledObject;
    public int pool = 0;
    public PooledObject[] poolObjects;
    private List<GameObject> objects;

    public Dictionary<string, List<GameObject>> pools;

	// Use this for initialization
	void Start () {
        instance = this;
        objects = new List<GameObject>();

        //// if pool is set, instantiate that amount of objects
        //if(this.pool != 0 && this.pool != -1)
        //{
        //    objects = this.InstantiatePooledObjects(pooledObject, pool);
        //}

        this.InitPools();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Instantiate certain amount of objects
    /// </summary>
    /// <param name="pooledObject">Object that will be instantiated</param>
    /// <param name="amount">Amount that needs to be instantiated</param>
    public List<GameObject> InstantiatePooledObjects(GameObject pooledObject, int amount)
    {
        List<GameObject> pool = new List<GameObject>();
        this.pooledObject = pooledObject;
        while(amount-- > 0)
        {
            GameObject obj = Instantiate<GameObject>(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
        return pool;
    }

    /// <summary>
    /// Returns first available pooled object
    /// </summary>
    /// <returns>Reference to a game object</returns>
    public GameObject GetPooledObject(string poolId)
    {
        List<GameObject> pool = pools[poolId];
        for(int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        /// if there are no free objects, instantiate and return a new instance
        GameObject obj = Instantiate<GameObject>(this.pooledObject);
        pool.Add(obj);
        return obj;
    }

    private void InitPools()
    {
        pools = new Dictionary<string, List<GameObject>>();
        foreach(PooledObject obj in poolObjects)
        {
            List<GameObject> pool = this.InstantiatePooledObjects(obj.prefab, obj.amount);
            pools.Add(obj.id, pool);
        }
    }
}

[System.Serializable]
public struct PooledObject
{
    public GameObject prefab;
    public int amount;
    public string id;
}


