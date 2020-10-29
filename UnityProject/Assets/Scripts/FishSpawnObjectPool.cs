using UnityEngine;
using System.Collections.Generic;

public class FishSpawnObjectPool : MonoBehaviour
{
    public int InitailSize = 5;
    //0:Whale、1:Nemo、2:StarFish，index越高，捕魚機率越高
    //public GameObject PrefabObj = null;
	private  GameObject spawnObj = null; //有問題
    private Queue<GameObject> _pool = new Queue<GameObject>();
    private int _objRandom = 0;

    //private Dictionary<GameObject, Fish> _fishMap = null;
    private void Update() {
        _objRandom =  Random.Range(0, FishOfSchoolManager.Instance.fishOfSchool.Length);
    }
    public void Init()
    {
        for (int i = 0; i < InitailSize; ++i)
		{
			GameObject spawnObj = Instantiate<GameObject>(FishOfSchoolManager.Instance.fishOfSchool[_objRandom]);
            //Fish fishCs = spawnObj.GetComponent<Fish>();
            //_fishMap.Add(spawnObj, fishCs);
			spawnObj.SetActive(false);

            _pool.Enqueue(spawnObj);
		}
    }
    public GameObject Spawn(Vector3 _position, Quaternion _rotation)
    {
        if(_pool.Count > 0){
            spawnObj = _pool.Dequeue();

            //Fish fishCs = GetFishCs(spawnObj);
            //fishCs._bezierBase._index = 0;
		    spawnObj.SetActive(true);
        }
        else{
            spawnObj = Instantiate<GameObject>(FishOfSchoolManager.Instance.fishOfSchool[_objRandom]);
            //Fish fishCs = spawnObj.GetComponent<Fish>();
            //_fishMap.Add(spawnObj, fishCs);
        }
        spawnObj.transform.position = _position;
        spawnObj.transform.rotation = _rotation;

		return spawnObj;
    }
    public void Despawn(GameObject _spawnObj)
    {
        _spawnObj.SetActive(false);
		_pool.Enqueue(_spawnObj);
    }

    // public Fish GetFishCs(GameObject fishObj)
    // {
    //     if (_fishMap.ContainsKey(fishObj) == false)
    //         return null;

    //     return _fishMap[fishObj];
    // }
}