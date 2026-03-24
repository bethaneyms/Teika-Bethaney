using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bckPrefab;
    public float speed = 1f;
    public float spacing = 10f;

    private GameObject[] bcks;

    void Start()
    {
        if (bckPrefab == null)
        {
            Debug.LogError("Background prefab is not assigned.");
            return;
        }

        bcks = new GameObject[3];

        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(i * spacing, i * spacing, 0f);
            bcks[i] = Instantiate(bckPrefab, pos, Quaternion.identity);
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

            bcks[i].transform.position += new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0f);

            if (bcks[i].transform.position.x < -spacing)
            {
                float farthestX = GetFarthestX();
                float farthestY = GetFarthestY();
                bcks[i].transform.position = new Vector3(farthestX + spacing, farthestY + spacing, 0f);
            }
        }
    }

    float GetFarthestX()
    {
        float farthest = bcks[0].transform.position.x;

        for (int i = 1; i < bcks.Length; i++)
        {
            if (bcks[i].transform.position.x > farthest)
                farthest = bcks[i].transform.position.x;
        }

        return farthest;
    }

    float GetFarthestY()
    {
        float farthest = bcks[0].transform.position.y;

        for (int i = 1; i < bcks.Length; i++)
        {
            if (bcks[i].transform.position.y > farthest)
                farthest = bcks[i].transform.position.y;
        }

        return farthest;
    }
}