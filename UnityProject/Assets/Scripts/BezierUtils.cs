using UnityEngine;

public class BezierUtils : MonoBehaviour
{ 
    /// <summary>
    /// 根據T值，計算貝塞爾曲線上面相對應的點
    /// </summary>
    /// <param name="t"></param>T值
    /// <param name="p0"></param>起始點
    /// <param name="p1"></param>控制點
    /// <param name="p2"></param>目標點
    /// <returns></returns>根據T值計算出來的貝賽爾曲線點
    public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }

    /// <summary>
    /// 獲取存儲貝塞爾曲線點的數組
    /// </summary>
    /// <param name="startPoint"></param>起始點
    /// <param name="controlPoint"></param>控制點
    /// <param name="endPoint"></param>目標點
    /// <param name="segmentNum"></param>采樣點的數量
    /// <returns></returns>存儲貝塞爾曲線點的數組
    public static Vector3[] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 1; i <= segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculateCubicBezierPoint(t, startPoint,
                controlPoint, endPoint);
            path[i - 1] = pixel;
        }
        return path;
    }
    public LineRenderer lineRenderer;
    private int layerOrder = 0;
    private int _segmentNum = 50;
    void DrawCurve()
    {
        for (int i = 1; i <= _segmentNum; i++)
        {
            float t = i / (float)_segmentNum;
            Vector3 pixelLineRenderer = CalculateCubicBezierPoint(t,
                SpawnPointManager.Instance.spawnPointManager[0].transform.position,
                SpawnPointManager.Instance.spawnPointManager[1].transform.position,
                SpawnPointManager.Instance.spawnPointManager[2].transform.position);
            lineRenderer.positionCount = i;
            lineRenderer.SetPosition(i - 1, pixelLineRenderer);
        }
    }
    private void Awake()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;
    }
    void Update()
    {
        DrawCurve();
    }
}