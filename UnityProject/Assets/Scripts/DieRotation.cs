using UnityEngine;
using UnityEngine.UI;

public class DieRotation :MonoBehaviour
{
    public Text ZoomText =null;
    public Animator ZoomAnimator= null;
    public SpriteRenderer _spriteRenderer = null;
    private FishCapacity _fishCapacity =null;
    private float _click = 0;
    private float _tmp = 0;
    private void Awake() {
        _fishCapacity = FindObjectOfType<FishCapacity>();
    }
    public void DieFishRotation(){
        _click += Time.deltaTime;
        if (_click >= 0.1f)
        {
            _click = 0;
            _tmp = Random.Range(0, FishOfSchoolManager.Instance.fishOfSchool.Length);
        }
        for (int i = 0; i < _tmp; i++)
        {
            _spriteRenderer = FishOfSchoolManager.Instance.fishOfSchool[i].GetComponent<SpriteRenderer>();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _spriteRenderer.sprite;
        }
        transform.Rotate(0, 0, 90);
    }
    public void ZoomIn(){
        ZoomAnimator.SetTrigger("FishZoom");
        ZoomText.text = _fishCapacity._fishMap[_fishCapacity.FishObjKey].FishPoint+"";
    }
}
