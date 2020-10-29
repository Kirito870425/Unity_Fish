using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    public float speed;
    public float LockSpeed = 0;
    private Rigidbody2D rigi;
    private Vector2 moveDir;
    private Cannon _canonCs=null;
    private float _tmp = 0;
    private Vector2 Trackbullet;
    private float distance = 0;
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        _canonCs =FindObjectOfType<Cannon>();
    }
    private void LockOn(){
        Trackbullet = _canonCs.FishTarget.transform.position - transform.position;
        distance = Trackbullet.magnitude; //子彈與目標.回傳距離

        _tmp+=Time.deltaTime;
        if(_tmp >=0.25f &&_canonCs._lockbool){
            _tmp = 0;
            rigi.velocity *= (distance == 0f ? 0f : LockSpeed / distance);
        }
    }
    private void FixedUpdate()
    {
        moveDir = transform.TransformDirection(new Vector2(speed * Time.deltaTime, 0)); //轉世界座標(local)
        rigi.velocity = moveDir;
        if(_canonCs._lockArea)
            LockOn();
    }

    private void OnCollisionEnter2D(Collision2D collision) //牆壁反彈
    {
        Vector3 reflVector3;
        if (collision.gameObject.tag == "Wall")
        {
            //Reflect(入射, 法線中心線)
            reflVector3 = Vector3.Reflect(moveDir.normalized, collision.contacts[0].normal);
            transform.right = reflVector3;
        }
    }
}
