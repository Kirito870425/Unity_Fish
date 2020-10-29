using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    [SerializeField]
    public int _tmpTakeZero = 0;
    public float FishSpawnSpeed = 0;
    private FishCapacity _fishCapacity;
    private void Awake() {
        _fishCapacity = FindObjectOfType<FishCapacity>();
        InvokeRepeating("SpawnFishPoint", 0, FishSpawnSpeed);
    }
    // private void Update() {
    //     if(Input.GetKeyUp(KeyCode.Space)){
    //         SpawnFishPoint();
    //     }
    // }
    private void SpawnFishPoint(){
        int _tmpRandom =  Random.Range(0, SpawnPointManager.Instance.spawnPointManager.Length);
        int _tmpTakeFish = _tmpRandom / 3;
        switch (_tmpTakeFish)
        {
            case 0:
                _fishCapacity.SpawnFish(SpawnPointManager.Instance.spawnPointManager[0].transform);
                _tmpTakeZero = 0;
                break;
            case 1:
                _fishCapacity.SpawnFish(SpawnPointManager.Instance.spawnPointManager[3].transform);
                _tmpTakeZero = 3;
                break;
            case 2:
                _fishCapacity.SpawnFish(SpawnPointManager.Instance.spawnPointManager[6].transform);
                _tmpTakeZero = 6;
                break;
            default:
                print("我不生魚啦 JOJO");
                break;
        }
    }
}
