using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        StartCoroutine(Init());
    }
    private IEnumerator Init()
    {
        while (ScoreManager.Instance == null)
        {
            yield return null;
        }

        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        UpdateScore(ScoreManager.Instance.Total);
    }
    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;

    }
    private void UpdateScore(int newScore)
    {
        _scoreText.text = newScore.ToString() + "tk";
    }
}