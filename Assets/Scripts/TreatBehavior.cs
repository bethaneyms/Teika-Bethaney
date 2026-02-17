using UnityEngine;

public class TreatBehavior : MonoBehaviour {

  public GameObject[] treats;
  public int treatType;
    
    // public float speed = 10f;
    // public float jumpForce = 15f;
    
    // private Rigidbody rb;
    // private AudioSource audioSource; 

     void Start() {

        treats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>().treats;

    //     rb = GetComponent<Rigidbody>();
    //     audioSource = GetComponent<AudioSource>();
     }

     void Update() {
        
    //     if (Input.GetKeyDown(KeyCode.Space)) { 
    //         rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //     }
     }

     private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Treat")) {
            int otherType = other.gameObject.GetComponent<TreatBehavior>().treatType;
            if (otherType == treatType && treatType < treats.Length - 1) {
                if(gameObject.transform.position.x < other.transform.position.x
                || (gameObject.transform.position.x > other.transform.position.x)
                 && (gameObject.transform.position.y >= other.transform.position.y)){

                }

            }

                }
                //Create the merged one
                int choice = treatType + 1;
            GameObject currentTreat  = Instantiate(treats[choice], 
            position: Vector3.Lerp(gameObject.transform.position,
            other.gameObject.transform.position, t:0.5f), Quaternion.identity);
            currentTreat.GetComponent<Collider2D>().enabled = true;
            currentTreat.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

                 //Destory both things (treats)
                 Destroy(other.gameObject);
                 Destroy(gameObject);



            
        }
     

    // void FixedUpdate() {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");

    //     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //     rb.AddForce(movement * speed);
    // }
}
