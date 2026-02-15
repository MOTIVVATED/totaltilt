using UnityEngine;

[ExecuteAlways]

public class FitToCamera : MonoBehaviour
{
    private void Start()
    {
        Fit();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
            Fit();
    }
#endif

    private void Fit()
    {
        Camera cam = Camera.main;
        if (cam == null || !cam.orthographic) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        transform.localScale = new Vector3(
            worldWidth / spriteSize.x,
            worldHeight / spriteSize.y,
            1f
        );
    }
}
