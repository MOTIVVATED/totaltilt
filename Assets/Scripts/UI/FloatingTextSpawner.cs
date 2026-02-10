using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    [SerializeField] private FloatingScoreText prefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera mainCamera;

    public static FloatingTextSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void Spawn(int amount, Vector3 worldPos, FallingObjectType type)
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        var instance = Instantiate(prefab, canvas.transform);
        instance.transform.position = screenPos;

        switch (type)
        {
            case FallingObjectType.Good:
                break;
            case FallingObjectType.Bad:
                instance.Setup(amount, FallingObjectType.Bad);
                break;
            case FallingObjectType.type_1tk:
                instance.Setup(amount, FallingObjectType.type_1tk);
                break;
            case FallingObjectType.type_15tk:
                instance.Setup(amount, FallingObjectType.type_15tk);
                break;
            case FallingObjectType.type_25tk:
                instance.Setup(amount, FallingObjectType.type_25tk);
                break;
            case FallingObjectType.type_111tk:
                instance.Setup(amount, FallingObjectType.type_111tk);
                break;
        }
    }
}
