using UnityEngine;

public class AudioManager:MonoBehaviour
{
    public AudioSource audioSource = null;
    public AudioClip Fightting = null;
    public AudioClip Jelly = null;
    public AudioClip Gold = null;

    public void test(){
        audioSource.PlayOneShot(Jelly);
    }
}
