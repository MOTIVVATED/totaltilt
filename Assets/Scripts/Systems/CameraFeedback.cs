using System.Collections;
using UnityEngine;

public class CameraFeedback : MonoBehaviour
{
    [SerializeField] private float goodKick = 0.05f;
    [SerializeField] private float badShake = 0.15f;
    [SerializeField] private float duration = 0.12f;

    private Vector3 startPos;
    private Coroutine current;

    private void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    private IEnumerator Init()
    {
        while (ScoreManager.Instance == null || TiltManager.Instance == null)
            yield return null;
        ScoreManager.Instance.OnScoreChanged += OnGood;
        TiltManager.Instance.OnTiltChanged += OnBad;
    }
    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= OnGood;
     
        if (TiltManager.Instance != null)
            TiltManager.Instance.OnTiltChanged -= OnBad;
    }
    private void OnGood(int _)
    {
        Play(goodKick);
    }
    private void OnBad(int _)
    {
        Play(badShake);
    }
    private void Play(float strength)
    {
        if (current != null) StopCoroutine(current);
        current = StartCoroutine(Shake(strength));
    }
    private IEnumerator Shake(float strength)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            transform.position = startPos + (Vector3)(Random.insideUnitCircle * strength);
            yield return null;
        }
        transform.position = startPos;
    }
}
