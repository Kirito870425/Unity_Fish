using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Cannon : MonoBehaviour
{
    public bool _lockbool = false;
    public bool _lockArea = false;
    public float Speed = 0;
    public GameObject SniperScope = null;
    public float netLimitTime = 0;
    private float netOverTime = 0;
    private FishCapacity _fishCapacityScript;
    private NetSpawnObjectPool _netSpawnObjectPool;
    private NetCapacity _netCapacity;
    private PointManagel _pointManager =null;
    private Fish targetFishCs = null;
    private Rigidbody2D rigi;
    [Header("砲台旋轉速度"), Range(0, 10000)]
    public float turn;
    // public List<GameObject> _lockList = null;
    public GameObject FishTarget = null;
    public GameObject  _sniperScope = null;
    public bool SniperBool =false;
    public bool  _searchBool = false;
    private float _tmp = 0;
    private float _moneyValue = 10;
    // private float index =0;
    public Dictionary<GameObject, Fish> LocalFishMapD = null;
    public float MapTest=0;

    
    private void CannonMove(Vector3 test)
    {
        test.Normalize(); //轉成向量0~1
        float targetAgnle = Mathf.Atan2(test.y, test.x)*Mathf.Rad2Deg; //計算弧度

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAgnle), turn * Time.deltaTime); //跟著滑鼠旋轉
    }
    //滑鼠
    private void CanonTest(){
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        Vector3 newpos = Camera.main.ScreenToWorldPoint(mouse);
        Vector3 direction = newpos - transform.position;

        if(Input.GetKey(KeyCode.Mouse0))
            CannonMove(direction);
    }
    //開道具，自動瞄準最大倍率魚種
    private void CanonTest2(){
        if(FishTarget == null)
            return;
        
        float _relativePosX = FishTarget.transform.position.x - transform.position.x;
        float _relativePosY = FishTarget.transform.position.y - transform.position.y;
        Vector3 testFolserFish = new Vector3(_relativePosX, _relativePosY, 0);
        CannonMove(testFolserFish);
    }
    //發射
    private void Shoot()
    {
        bool mouse = Input.GetKey(KeyCode.Mouse0);
        if (mouse)
        {
            netOverTime += Time.deltaTime;
            if(netOverTime >= netLimitTime){
                netOverTime = 0;
                _netCapacity.SpawnFishNet(_netSpawnObjectPool.FirePointTransform);
            }
        }
    }
    
    //降冪排序。只取一隻，最大倍率魚
    private void TakeTheLargestFish(){
        foreach(KeyValuePair<GameObject, Fish> kvp in LocalFishMapD.OrderByDescending(x => x.Value))
        {
            if(_searchBool){
                CanonTest2(); //算方向
                targetFishCs = kvp.Value;
                FishTarget = kvp.Key; //鎖定魚
                print(targetFishCs.gameObject +""+ targetFishCs.fishdead);
            }
            _sniperScope.transform.position = FishTarget.transform.position;
            break;
        }
    }
    public void LockFish(){
        if(_lockbool && _lockArea){
            _fishCapacityScript._fishMap[_fishCapacityScript.FishObjKey].CapsuleCollider2D.enabled = false;
            
            _searchBool = true;
            
            _tmp +=Time.deltaTime;
            if(_tmp >=10f){
                _lockbool= false;
            }
        }
        
        if(!_lockbool || !_lockArea){
            _searchBool = false;
            _fishCapacityScript._fishMap[_fishCapacityScript.FishObjKey].CapsuleCollider2D.enabled = true;
        }
    }
    //public void LockButton(){
    //    _lockbool = true;
    //    _tmp = 0;
    //    _pointManager.Money.text = _moneyValue-- + "";
    //}
    private void Awake() {
        _fishCapacityScript = FindObjectOfType<FishCapacity>();
        _netCapacity = FindObjectOfType<NetCapacity>();
        _netSpawnObjectPool = FindObjectOfType<NetSpawnObjectPool>();
        _pointManager = FindObjectOfType<PointManagel>();

        LocalFishMapD = new Dictionary<GameObject, Fish>();
    }
    private void Update()
    {

        TakeTheLargestFish();
        
        LockFish();
        CanonTest();
        Shoot();
    }
}
