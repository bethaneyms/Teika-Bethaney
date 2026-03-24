using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public AudioSource clickSound;

    public void PlayGame()
    {
        if(clickSound != null)
            clickSound.Play();

        Invoke("LoadGame", 0.2f);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("TeikaGame");
    }
}