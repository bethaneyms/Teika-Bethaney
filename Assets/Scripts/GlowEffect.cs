using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public float pulseSpeed = 3f;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;
    public Color colorA = Color.white;
    public Color colorB = Color.yellow;

    private Vector3 baseScale;
    private SpriteRenderer sr;

    void Start()
    {
        baseScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float pulse = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;

        float scale = Mathf.Lerp(minScale, maxScale, pulse);
        transform.localScale = baseScale * scale;

        if (sr != null)
            sr.color = Color.Lerp(colorA, colorB, pulse);
    }
}