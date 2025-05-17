using UnityEngine;

public class EnemyScoreReporter : MonoBehaviour
{
    public int scoreValue = 50; // Set per prefab

    void OnEnable()
    {
        Target.OnAnyTargetDeath += OnTargetDeath;
    }

    void OnDisable()
    {
        Target.OnAnyTargetDeath -= OnTargetDeath;
    }

    void OnTargetDeath(Target dead)
    {
        if (dead.gameObject == this.gameObject)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
    }
}
