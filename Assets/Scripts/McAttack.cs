using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McAttack : MonoBehaviour
{
    public ParticleSystem dustAttack;

    public float attackRadius = 1.5f;
    public float attackDamage = 10.0f;
    public LayerMask attackLayerMask;

    // CapsuleCollider to define the attack range
    private CapsuleCollider attackRangeCollider;
    private LineRenderer lineRenderer;

    private Transform playerTransform;

    void Start()
    {
        // Create a CapsuleCollider and set its dimensions to define the attack range
        attackRangeCollider = gameObject.AddComponent<CapsuleCollider>();
        attackRangeCollider.height = attackRadius;
        attackRangeCollider.radius = attackRadius / 2;
        attackRangeCollider.direction = 2; // Set the capsule direction to z-axis
        attackRangeCollider.isTrigger = true; // Set the collider to trigger

        // Create a LineRenderer to draw the attack effect
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
            dustAttack.transform.position = playerTransform.position;
            dustAttack.Play();
        }
    }

    void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, attackLayerMask);

        foreach (Collider hit in hits)
        {
            // Check if the object hit has a health component
            Health health = hit.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }

        // Draw the attack effect
        DrawAttackEffect();
    }

    void DrawAttackEffect()
    {
        // Set the positions of the line renderer to draw a line in the direction of the attack
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position;
        positions[1] = transform.position + transform.forward * attackRadius;

        // Set the positions of the line renderer and enable it
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;

        // Disable the line renderer after a short delay
        Invoke("DisableLineRenderer", 0.1f);
    }

    void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
