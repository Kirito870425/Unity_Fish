using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCapacity : MonoBehaviour
{
    public GameObject SpawnRoot = null;
    private PointManagel _pointManagel;
    private NetSpawnObjectPool _netSpawnObjectPool = null;
    public List<GameObject> _activeObjList = null;
    //生成漁網
    public void SpawnFishNet(Transform _netparent){
        _pointManagel.PointGet(0, PointManagel.firePointSwitch);

        GameObject testObj = _netSpawnObjectPool.Spawn(_netparent.position, _netparent.rotation);
        testObj.transform.SetParent(SpawnRoot.transform);

        _activeObjList.Add(testObj);
    }
    //回收漁網
    public void DeSpawnFishNet(GameObject _destroy){
        if (_activeObjList.Count == 0)
            return;

        _netSpawnObjectPool.Despawn(_destroy);

        _activeObjList.RemoveAt(0);
    }
    private void Awake() {
         _activeObjList = new List<GameObject>();
        _pointManagel = FindObjectOfType<PointManagel>();
        _netSpawnObjectPool = FindObjectOfType<NetSpawnObjectPool>();
        _netSpawnObjectPool.Init();
    }
}
