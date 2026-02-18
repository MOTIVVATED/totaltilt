using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnRule
    {
        public FallingObject prefab;
        public float delayMin = 1f;
        public float delayMax = 3f;
        public bool enabled = true;
    }

    [Header("Spawn rules (per prefab)")]
    [SerializeField] private SpawnRule[] rules;

    [Header("Spawn position")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float minX, maxX;

    [Header("Spawn limits")]
    [SerializeField] private float minGlobalSpawnInterval = 0.5f;

    [SerializeField] private Sprite[] badSprites;

    [SerializeField] private bool subscribeOnEvents;

    private Coroutine[] routines;

    private float nextAllowedSpawnTime;

    private void OnEnable()
    {
        if (rules == null || rules.Length == 0)
        {
            Debug.LogWarning("No spawn rules assigned to SpawnManager.");
            enabled = false;
            return;
        }

        nextAllowedSpawnTime = Time.time;

        routines = new Coroutine[rules.Length];

        for (int i = 0; i < rules.Length; i++)
        {
            if (rules[i] != null && rules[i].enabled && rules[i].prefab != null)
                routines[i] = StartCoroutine(SpawnRuleLoop(rules[i]));
        }
    }

    private void OnDisable()
    {
        if (routines == null) return;
        for (int i = 0; i < routines.Length; i++)
        {
            if (routines[i] != null) StopCoroutine(routines[i]);
        }
    }

    private IEnumerator SpawnRuleLoop(SpawnRule rule)
    {
        while (true)
        {
            float delay = Random.Range(rule.delayMin, rule.delayMax);
            yield return new WaitForSeconds(delay);

            if (Time.time < nextAllowedSpawnTime)
                continue;

            Spawn(rule.prefab);

            nextAllowedSpawnTime = Time.time + minGlobalSpawnInterval;
        }
    }
    private void Spawn(FallingObject prefab)
    {
        FallingObject falling = Instantiate(
            prefab, spawnPoint.position, Quaternion.identity);

        float randomX = Random.Range(minX, maxX);

        falling.transform.position = new Vector3(randomX, spawnPoint.position.y, 0f);

        if (falling.ObjectType == FallingObjectType.Bad
            && badSprites != null && badSprites.Length > 0)
        {
            falling.SetSprite(badSprites[Random.Range(0, badSprites.Length)]);
        }
        if (subscribeOnEvents)
        {
            falling.OnCollected += ScoreManager.Instance.HandleCollected;

            falling.OnCollected += TiltManager.Instance.HandleCollected;
            falling.OnMissed += TiltManager.Instance.HandleMissed;

            falling.OnSmashed += SmashManager.Instance.HandleSmashed;

            falling.OnSmashed += TiltManager.Instance.HandleSmashed;
        }
    }
}
