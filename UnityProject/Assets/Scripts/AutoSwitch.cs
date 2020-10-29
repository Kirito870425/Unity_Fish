using UnityEngine;
using UnityEngine.UI;

public class AutoSwitch : MonoBehaviour
{
    public Text AutoText;
    public Image autoImage;
    public float netLimitTime;
    private float netOverTime;
    private bool autoswitch;
    private NetCapacity _netCapacity;
    private NetSpawnObjectPool _netSpawnObjectPool;

    public void AutoButton(){
        if(!autoswitch){
            autoImage.color = Color.red;
            AutoText.text = "開啟自動";
            autoswitch = true;

        }else{
            autoImage.color = Color.green;
            AutoText.text = "關閉自動";
            autoswitch = false;
        }
    }
    private void AutoSpawnNet(){
        if(autoswitch)
        {
            netOverTime += Time.deltaTime;
            if(netOverTime >= netLimitTime)
            {
                netOverTime = 0;
                _netCapacity.SpawnFishNet(_netSpawnObjectPool.FirePointTransform);
            }
        }
    }
    private void Awake() {
        _netSpawnObjectPool = FindObjectOfType<NetSpawnObjectPool>();
        _netCapacity = FindObjectOfType<NetCapacity>();
        autoImage.color = Color.green;
    }
    private void Update(){
        AutoSpawnNet();
    }
}
