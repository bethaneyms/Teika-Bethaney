using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public Sprite[] UISprites;
    public int[] queue;
    private SpriteRenderer[] childRenderers;

    void Start()
    {
        queue = new int[4];
        for (int i = 0; i < 4; i++)
        {
            queue[i] = Random.Range(0, UISprites.Length);
        }
        
        childRenderers = new SpriteRenderer[4];
        for (int i = 0; i < transform.childCount && i < 4; i++)
        {
            childRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (childRenderers[i] != null)
                childRenderers[i].sprite = UISprites[queue[i]];
        }
    }

    public int updateQueue()
    {
        int currentType = queue[0];

        for (int i = 1; i < 4; i++) {
            queue[i-1] = queue[i];
        }
        queue[3] = Random.Range(0, UISprites.Length);
        return currentType;
    }
}