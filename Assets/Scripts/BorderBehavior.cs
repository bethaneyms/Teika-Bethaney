using UnityEngine;

public class BorderBehavior : MonoBehaviour
{
    public float timeout = 3f;
    private float timeStart;
    public GameObject gameOver;


    void Start(){ 

    } 
    void Update(){

     }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Treat")){
            timeStart = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("Treat")){
            float timeThusfar = Time.time - timeStart;
            if (timeThusfar > timeout){
                print("game over dude");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){

    }
}
