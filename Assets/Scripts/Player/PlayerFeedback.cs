using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    [SerializeField] private Transform playerVisual;
    [SerializeField] private float punchScale = 1.1f;
    [SerializeField] private float duration = 0.1f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = playerVisual.localScale;   
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    private System.Collections.IEnumerator Init()
    {
        while (ScoreManager.Instance == null || TiltManager.Instance == null)
            yield return null;

        ScoreManager.Instance.OnScoreChanged += OnGood;
        TiltManager.Instance.OnTiltIncreased += OnBad;
    }
    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= OnGood;
        
        if (TiltManager.Instance != null)
            TiltManager.Instance.OnTiltIncreased -= OnBad;
    }
    private void OnGood(int _)
    {
        StopAllCoroutines();
        StartCoroutine(Punch(originalScale * punchScale));
    }
    private void OnBad(int _)
    {
        StopAllCoroutines();
        StartCoroutine(Punch(originalScale * 0.9f));
    }
    private System.Collections.IEnumerator Punch(Vector3 target)
    {
        playerVisual.localScale = target;

        yield return new WaitForSeconds(duration);

        playerVisual.localScale = originalScale;
    }
}
