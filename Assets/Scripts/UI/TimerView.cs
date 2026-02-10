using System.Collections;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        StartCoroutine(Init());
    }
    private IEnumerator Init()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        GameManager.Instance.OnTimeChanged += UpdateTimer;
        UpdateTimer(GameManager.Instance.Timer, GameManager.Instance.GameDuration);
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnTimeChanged += UpdateTimer;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnTimeChanged -= UpdateTimer;
    }

    private void UpdateTimer(float elapsed, float duration)
    {
        int e = Mathf.FloorToInt(elapsed)/10;
        int d = Mathf.FloorToInt(duration)/10;
        timerText.text = $"{e}/{d}h";
    }
}
