using UnityEngine;
using System.Collections.Generic;

public class NetSpawnObjectPool : MonoBehaviour
{
    public GameObject PrefabObj = null;
    [Header("漁網生成地點")]
    public Transform FirePointTransform;
    public int InitailSize = 1;
    
    private Queue<GameObject> _pool = new Queue<GameObject>();
    
    public void Init()
    {
        for (int i = 0; i < InitailSize; ++i)
		{
			GameObject spawnObj = Instantiate<GameObject>(PrefabObj);
			spawnObj.transform.SetParent(this.transform);
			spawnObj.SetActive(false);

            _pool.Enqueue(spawnObj);
		}
    }
    public GameObject Spawn(Vector3 _position, Quaternion _rotation)
    {
		GameObject spawnObj = null;

        if(_pool.Count > 0)
            spawnObj = _pool.Dequeue();
        else
            spawnObj = Instantiate<GameObject>(PrefabObj);

        spawnObj.transform.position = _position;
        spawnObj.transform.rotation = _rotation;
		spawnObj.transform.SetParent(null);
		spawnObj.SetActive(true);

		return spawnObj;
    }
 
    public void Despawn(GameObject spawnObj)
    {
		spawnObj.transform.SetParent(this.transform);
        spawnObj.SetActive(false);

		_pool.Enqueue(spawnObj);
    }
}