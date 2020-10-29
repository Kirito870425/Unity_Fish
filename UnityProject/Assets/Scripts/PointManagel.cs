using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PointManagel : MonoBehaviour
{
    private FishCapacity _fishCapecity = null;
    private double points;
    public Text Money= null;
    public Text pointtext; //左下$$
    public Text firePointSwitchText; //右下切換消耗
    public static int firePointSwitch;
    public bool test = false;
    public void LeftButton(){
        firePointSwitchText.text = (firePointSwitch -= 250) + "";
    }
    public void RightButton(){
        firePointSwitchText.text = (firePointSwitch += 250) + "";
    }
    public void PointGet(double _killPoint, double _firePoint)
    {
        points += _killPoint;
        points -= _firePoint;
    }
    private void PointsInit(){
        firePointSwitchText.text = firePointSwitch + "";
        pointtext.text = points + "";
        if (firePointSwitch < 1)
        {
            firePointSwitch = 0;
        }
        if(points < 0){
            points = 0;
        }
    }
    public void ShowGetGold(){

    }
    public void PointJudgment(){
        if(firePointSwitch >=1000 && !test){
            for (int i = 0; i < FishOfSchoolManager.Instance.fishOfSchool.Length; i++)
            {
                FishOfSchoolManager.Instance.fishOfSchool[i].GetComponent<Fish>().fishdead -= 0.1f;
                print("機率上升10%");
            }
            test = true;
        }
        else if(firePointSwitch < 500 && test){
            for (int i = 0; i < FishOfSchoolManager.Instance.fishOfSchool.Length; i++)
            {
                FishOfSchoolManager.Instance.fishOfSchool[i].GetComponent<Fish>().fishdead += 0.1f;
                print("機率下降10%");
            }
            test = false;
        }
    }
    private void Awake() {
        _fishCapecity = FindObjectOfType<FishCapacity>();
    }
    private void Update()
    {
        PointsInit();
        PointJudgment();
    }
}
