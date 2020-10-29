using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCapacity : MonoBehaviour
{
    public List<GameObject> _activeObjFishList = null;
    private FishSpawnObjectPool _fishSpawnObjectPool = null;

    public Dictionary<GameObject, Fish> _fishMap = null;
    public GameObject FishObjKey = null;

    public void SpawnFish(Transform _parent){
        
        GameObject fishObj = _fishSpawnObjectPool.Spawn(_parent.position, _parent.rotation); //spawn有SA(t)、Instantiate
        // 判斷Map是否存在Fish，不存在就GetComponent<Fish>
        if(_fishMap.ContainsKey(fishObj) == false)
        {
             Fish fishCs = fishObj.GetComponent<Fish>();
             _fishMap.Add(fishObj, fishCs);
        }
        FishObjKey = fishObj;
        // 使用Fish清除bezier index
        _fishMap[fishObj]._bezierBase._index = 0;
        _fishMap[fishObj]._fishSpriteRenderer.color = Color.white;
        _fishMap[fishObj].enabled = true;
        _fishMap[fishObj].CapsuleCollider2D.enabled =true;
        _fishMap[fishObj].transform.rotation = transform.rotation;

        if (fishObj.transform.parent != _parent) //判斷生成的物件有沒有在傳入_parent底下(出生點)
            fishObj.transform.SetParent (_parent);
        
        _activeObjFishList.Add(fishObj); //clone fish
    }
    public void DeSpawnFish(){
        if (_activeObjFishList.Count == 0){ 
            return;
        }
        else{
            GameObject go = _activeObjFishList[0];
            _fishSpawnObjectPool.Despawn(go); //Despawn裡有SA(f)
            _activeObjFishList.RemoveAt(0);
        }
    }
    private void Awake() {

        _fishMap = new Dictionary<GameObject, Fish>();

        _activeObjFishList = new List<GameObject>();
        _fishSpawnObjectPool = FindObjectOfType<FishSpawnObjectPool>();
        _fishSpawnObjectPool.Init();
    }
}
