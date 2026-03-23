using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bckPrefab;
    public float speed = 1f;
    public float spacing = 16f;
    private GameObject[] bcks;

    void Start()
    {
        bcks = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(i * spacing, 0f, 0f);
            bcks[i] = Instantiate(bckPrefab, pos, Quaternion.identity);
        }
    }

    void Update()
    {
        for (int i = 0; i < bcks.Length; i++)
        {
            bcks[i].transform.position += Vector3.left * speed * Time.deltaTime;

            if (bcks[i].transform.position.x < -spacing)
            {
                float rightMostX = bcks[0].transform.position.x;
                for (int j = 1; j < bcks.Length; j++)
                {
                    if (bcks[j].transform.position.x > rightMostX)
                        rightMostX = bcks[j].transform.position.x;
                }

                bcks[i].transform.position = new Vector3(rightMostX + spacing, 0f, 0f);
            }
        }
    }
}