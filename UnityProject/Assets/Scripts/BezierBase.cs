using UnityEngine;
public class BezierBase
{
    //貝賽爾曲線的路徑點的數量，數量越多獲得的曲線越完美，但是消耗也就越大
    private int _segmentNum = 100;
    //獲得貝賽爾曲線所有的路徑點
    private Vector3[] _allposArry;
    //移動的速度
    private float _speed = 10;
    //所有的路徑點的索引值
    public int _index = 0;
    //需要移動的目標
    private Transform _moveTargte;
    //是否完成移動
    private bool _isMoveComplete = false;
    /// <summary>
    ///  初始化貝賽爾曲線所有的路徑點
    /// </summary>
    /// <param name="startPos">開始移動的位置</param>
    /// <param name="controlPos">中間點的位置，目的是改變物體移動的曲線弧度</param>
    /// <param name="endPos">最終移動的位置</param>
    /// <param name="moveTarget">需要移動的目標物體</param>
    /// <param name="moveSpeed">移動速度</param>
    /// <param name="segmentNum">三點之間Bezier曲線上所有點的數量</param>
    public void Init(Vector3 startPos, Vector3 controlPos, Vector3 endPos,Transform moveTarget,float moveSpeed,int segmentNum=100)
    {
        _moveTargte = moveTarget;
        _speed = moveSpeed;
        _allposArry = new Vector3[segmentNum];
        _allposArry = BezierUtils.GetBeizerList(startPos, controlPos, endPos, _segmentNum);
        _isMoveComplete = false;
    }
    public delegate void BezierMoveCompleteDel();
    public void Move(BezierMoveCompleteDel moveCompleteDel = null)
    {
        if (_allposArry == null|| _allposArry.Length==0)
        {
            Debug.LogError("沒有初始化貝賽爾曲線的所有的路徑點");
            return;
        }
        if(!_isMoveComplete)
        {
            if (Vector3.Distance(_moveTargte.position, _allposArry[_index]) <= 0.1f)
            {
                _index++;
                if (_index > _segmentNum - 1)
                {
                    _index = _segmentNum - 1;
                    if (moveCompleteDel != null)
                    {
                        moveCompleteDel();
                        _index = 0;
                    }
                    //_isMoveComplete = true;
                }
            }
            else
            {
                _moveTargte.position = Vector3.Slerp(_moveTargte.position, _allposArry[_index], _speed * Time.deltaTime);
            }
        }
    }
}