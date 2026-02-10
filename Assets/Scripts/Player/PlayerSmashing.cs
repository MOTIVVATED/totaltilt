using UnityEditor;
using UnityEngine;
using System;

public class PlayerSmashing : MonoBehaviour
{
    [Header("Ban settings")]
    [SerializeField] private float smashRadius = 2f;
    [SerializeField] private LayerMask badLayer;

    public event Action OnSmash;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Smash();
        }
    }
    private void Smash()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position, 
            smashRadius, 
            badLayer
        );
        foreach (var hit in hits)
        {
            FallingObject bad = hit.GetComponent<FallingObject>();

            if (bad != null)
            {
                bad.Smash();
            }
        }
        OnSmash?.Invoke();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smashRadius);
    }
}
