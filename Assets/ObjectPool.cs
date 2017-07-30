using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool instance;
    public GameObject pooledObject;
    public PooledObject[] poolObjects;

    public Dictionary<string, List<GameObject>> pools;

	// Use this for initialization
	void Start () {
        instance = this;
        this.InitPools();
    }
	
    /// <summary>
    /// Instantiate certain amount of objects
    /// </summary>
    /// <param name="pooledObject">Object that will be instantiated</param>
    /// <param name="amount">Amount that needs to be instantiated</param>
    /// <returns>List of instatiated objects</returns>
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
    /// Return first available object in a pool
    /// </summary>
    /// <param name="poolId">String that identifies pool</param>
    /// <returns>Free GameObject reference</returns>
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

    /// <summary>
    /// Instantiates all pools and fills them with free objects
    /// </summary>
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

/// <summary>
/// Structure that describes single object pool
/// </summary>
[System.Serializable]
public struct PooledObject
{
    public GameObject prefab;
    public int amount;
    public string id;
}


