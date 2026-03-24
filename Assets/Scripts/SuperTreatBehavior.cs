using UnityEngine;

public class SuperTreatBehavior : MonoBehaviour
{
    public int cupcakeType = 6;
    public int pointsPerCupcake = 25;

    private bool hasActivated = false;
    private AudioSource superSource;

    void Start()
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag("SuperSound");
        if (soundObject != null)
            superSource = soundObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasActivated)
            return;

        TreatBehavior treat = other.gameObject.GetComponent<TreatBehavior>();

        if (treat != null && treat.treatType == cupcakeType)
        {
            hasActivated = true;

            GameObject[] allTreats = GameObject.FindGameObjectsWithTag("Treat");
            int cupcakesCleared = 0;

            foreach (GameObject obj in allTreats)
            {
                TreatBehavior tb = obj.GetComponent<TreatBehavior>();

                if (tb != null && tb.treatType == cupcakeType)
                {
                    cupcakesCleared++;
                    Destroy(obj);
                }
            }

            if (superSource != null && superSource.clip != null)
                superSource.PlayOneShot(superSource.clip);

            PlayerBehavior player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();

            if (player != null)
            {
                int bonus = cupcakesCleared * pointsPerCupcake;
                player.AddBonusScore(bonus);
            }

            Destroy(gameObject);
        }
    }
}