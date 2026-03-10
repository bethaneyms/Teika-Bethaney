using UnityEngine;

public GameObject bckPrefab;
public float speed;
private GameObject[] bcks;
public float pivoitPoint;

public class BackgroundManager : MonoBehaviour
{

    void Start()
    {
        bcks = new GameObject[3];

        float xPos = pivoitPoint - (pivoitPoint / 2 * i);
        float yPos = pivoitPoint - (pivoitPoint / 2 * i);
        for (int i = 0; i < 3; i++) {
            bcks[i] = Instantiate(bckPrefab, position, Quaternion.identity);
        }
        Vector3 position = new Vector3(xPos, yPos, 0.0f);
    }

    void Update()
    {
        
    }
}
