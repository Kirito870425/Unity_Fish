using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;
public class Fish : MonoBehaviour
{
    [Header("捕魚機率")]
    public float fishdead =0;
    public float Speed = 10;
    [Header("魚的分數"), Range(0, 100000)]
    public double FishPoint = 0;
    public BezierBase _bezierBase=null;
    public SpriteRenderer _fishSpriteRenderer=null;
    public GameObject Gold =null;
    public CapsuleCollider2D CapsuleCollider2D = null;
    public bool tsetbool = false;
    
    
#region 私人宣告區
    private Animator DieFishAnimator = null;
    private SpawnFish spawnFish = null;
    private FishCapacity _fishCapacity = null;
    private NetCapacity _netCapacity = null;
    private NetSpawnObjectPool _netSpawnObjectPool = null;
    private PointManagel _pointManagel = null;
    private DieRotation _dieRotation = null;
    private AudioManager _audioManager = null;
    private Cannon _canonCs = null;
    int _segmentNum = 100;
    private Vector3[] posArry;
#endregion
    
    public void KillFish()
    {
        tsetbool = false;
        float r = Random.Range(0f, 1f);
        if (r >= fishdead)
        {
            CapsuleCollider2D.enabled = false;
            _pointManagel.PointGet(FishPoint, 0); //傳回分數
            _dieRotation.ZoomIn(); //縮放字
            Instantiate(Gold, this.transform.position, this.transform.rotation); //掉錢
            _audioManager.test(); //聲音
            DieFishAnimator.SetTrigger("FishRotation");
            // tsetbool = true;

            _fishCapacity.DeSpawnFish();
            this.enabled =false;
        }
    }
    private void HitFish(){
        _fishSpriteRenderer.color = Color.red;
    }
    private void FishInit(){
        _bezierBase.Init(SpawnPointManager.Instance.spawnPointManager[spawnFish._tmpTakeZero].transform.position,
            SpawnPointManager.Instance.spawnPointManager[spawnFish._tmpTakeZero + 1].transform.position,
            SpawnPointManager.Instance.spawnPointManager[spawnFish._tmpTakeZero + 2].transform.position,
            this.transform, Speed, _segmentNum);
    }
    private void Awake() {
        _bezierBase = new BezierBase(); //沒繼承Mono， 有Mono可以In GetC可以Instantiate + GetC
        _fishCapacity = FindObjectOfType<FishCapacity>();
        _netCapacity = FindObjectOfType<NetCapacity>();
        _netSpawnObjectPool = FindObjectOfType<NetSpawnObjectPool>();
        _pointManagel = FindObjectOfType<PointManagel>();
        spawnFish = FindObjectOfType<SpawnFish>();
        _dieRotation = FindObjectOfType<DieRotation>();
        _audioManager = FindObjectOfType<AudioManager>();
        _canonCs = FindObjectOfType<Cannon>();
        DieFishAnimator = GetComponent<Animator>();
        
        this.enabled = true;
        FishInit();
    }
    private void OnComplete(){
        _fishCapacity.DeSpawnFish();
    }
    void Update()
    {
        _bezierBase.Move(OnComplete);
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "FishNet"){
            HitFish();
            KillFish();
        }
        if(collision.name == "LockArea"){
            _canonCs.LocalFishMapD.Add(gameObject, _canonCs.MapTest);

            _canonCs._lockArea = true;
            // _canonCs._searchBool = true;
            print("進");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "FishNet"){

            _fishSpriteRenderer.color = Color.white;
            _netCapacity.DeSpawnFishNet(other.gameObject);
            this.enabled = true;
        }
        if(other.name == "LockArea"){
            _canonCs.LocalFishMapD.Remove(gameObject);

            _canonCs._lockArea = false;
            _canonCs._searchBool = false;
            print("出");
        }
    }
}
