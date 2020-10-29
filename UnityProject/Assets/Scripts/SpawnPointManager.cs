using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoSingletonBase<SpawnPointManager>
{
    [Header("生成點管理")]
    public GameObject[] spawnPointManager = null;

    public override void Init()
    {
    }
    public override void Release()
    {
    }
}
