using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
        public void goToGame(){
        SceneManager.LoadScene("TeikaGame");
    }

}
