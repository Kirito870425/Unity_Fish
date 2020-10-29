using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOfSchoolManager : MonoSingletonBase<FishOfSchoolManager>
{
    [Header("魚群種類管理")]
    public GameObject[] fishOfSchool = null;

    public override void Init()
    {
    }
    public override void Release()
    {
    }
}
