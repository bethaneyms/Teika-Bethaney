using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bckPrefab;
    public float speed;
    private GameObject[] bcks;
    public float pivotPoint;
    public float scale;

    void Start()
    {
        if (bckPrefab == null)
        {
            Debug.LogError("Background prefab is not assigned.");
            return;
        }

        pivotPoint = scale * 16 * -0.32f;
        bckPrefab.transform.localScale = new Vector3(scale, scale, scale);

        bcks = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            float xPos = pivotPoint - (pivotPoint / 2 * i);
            float yPos = pivotPoint - (pivotPoint / 2 * i);
            Vector3 position = new Vector3(xPos, yPos, 0.0f);

            bcks[i] = Instantiate(bckPrefab, position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (bcks == null)
            return;

        for (int i = 0; i < bcks.Length; i++)
        {
            if (bcks[i] == null)
                continue;

            float xPos = bcks[i].transform.position.x + speed * Time.deltaTime;
            float yPos = bcks[i].transform.position.y + speed * Time.deltaTime;
            Vector3 position = new Vector3(xPos, yPos, 0.0f);

            if (bcks[i].transform.position.x < -pivotPoint / 2)
            {
                position = new Vector3(pivotPoint, pivotPoint, 0.0f);
            }

            bcks[i].transform.position = position;
        }
    }
}