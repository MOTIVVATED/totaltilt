using System.Collections;
using TMPro;
using UnityEngine;

public class ViewersView : MonoBehaviour
{
    [SerializeField] private TMP_Text viewersText;
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

        GameManager.Instance.OnViewersChanged += UpdateViewers;
        UpdateViewers(Time.timeScale);
    }
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnViewersChanged += UpdateViewers;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnViewersChanged -= UpdateViewers;
    }

    private void UpdateViewers(float timeScale)
    {
        int viewers = (Mathf.FloorToInt(300 * timeScale) + Random.Range (-5,5));
        
        viewersText.text = $"{viewers}";
    }
}
