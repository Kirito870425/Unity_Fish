using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void DelayStartButton(){
        Invoke("StartButton", 0.5f);
    }
    public void DelayBackButton(){
        Invoke("BackButton", 0.5f);
    }
    public void DelayExitButton(){
        Invoke("ExitButton", 0.5f);
    }
    private void StartButton(){
        SceneManager.LoadScene(1);
    }
    private void BackButton(){
        SceneManager.LoadScene(0);
    }
    private void ExitButton(){
        Application.Quit();
    }
}
