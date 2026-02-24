using UnityEngine;

public class TreatBehavior : MonoBehaviour
{
    public float timeout;
    public float timeStart;

    public GameObject[] treats;
    public int treatType;


    void Start(){ 

        treats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().treats;

    } 
    void Update(){

     }

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("fruit")){
            int otherType = other.gameObject.GetComponent<TreatBehavior>().treatType;
            if(otherType == treatType &&  treatType < treats.Length -1){
                //Destroy both thigs and create the merged one
                if(gameObject.transform.position.x<= other.transform.position.x || 
                (gameObject.transform.position.x== other.transform.position.x && gameObject.transform.position.y>= other.transform.position.y)){
                    int choice = treatType+1;

                    



                    GameObject currenttreat  = Instantiate(treats[choice], Vector3.Lerp(gameObject.transform.position, other.gameObject.transform.position, 0.5f), Quaternion.identity);
                    currenttreat.GetComponent<Collider2D>().enabled = true;
                    currenttreat.GetComponent<Rigidbody2D>().gravityScale =1.0f;


                   // GetComponent<AudioSource>()


                    GameObject.FindGameObjectWithTag("Player").
                    GetComponent<PlayerBehavior>().updateScore(treatType);

                    Destroy(other.gameObject);
                    Destroy(gameObject);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Top")){
            timeStart = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("Top")){
            float timeThusfar = Time.time - timeStart;
            if (timeThusfar > timeout){
                
                print("game over dude");

            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("Top")){
            timeStart = 0f;
        }
    }
}
