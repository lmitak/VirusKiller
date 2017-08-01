using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour {

	public void Explode(Vector3 atPosition)
    {
        GameObject obj = ObjectPool.instance.GetPooledObject("enemyExplosion");
        obj.transform.parent = this.transform;
        obj.transform.position = atPosition;
        obj.SetActive(true);
    }
}
