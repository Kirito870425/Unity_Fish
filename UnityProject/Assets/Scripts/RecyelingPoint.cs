using UnityEngine;

public class RecyelingPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag =="Gold"){
            Destroy(other.gameObject);
        }
    }
}
