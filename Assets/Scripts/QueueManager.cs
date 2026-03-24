using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public Sprite[] UISprites;
    public int[] queue;

    private SpriteRenderer[] childRenderers;

    void Start()
    {
        queue = new int[4];

        for (int i = 0; i < queue.Length; i++)
        {
            queue[i] = Random.Range(0, UISprites.Length);
        }

        childRenderers = new SpriteRenderer[4];

        for (int i = 0; i < 4; i++)
        {
            childRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        UpdateQueueVisuals();
    }

    void UpdateQueueVisuals()
    {
        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (childRenderers[i] != null &&
                queue[i] >= 0 &&
                queue[i] < UISprites.Length)
            {
                childRenderers[i].sprite = UISprites[queue[i]];
            }
        }
    }

    public int updateQueue()
    {
        int currentTreat = queue[0];

        for (int i = 1; i < queue.Length; i++)
        {
            queue[i - 1] = queue[i];
        }

        queue[queue.Length - 1] = Random.Range(0, UISprites.Length);

        UpdateQueueVisuals();

        return currentTreat;
    }
}