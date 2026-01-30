using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    [Header("References")]
    public Transform puck;

    [Header("Movement")]
    public float speed = 6f;          // Overall movement speed
    public float minY = 0.3f;          // AI side boundary
    public float maxY = 3.4f;             // Top boundary

    [Header("Attack")]
    public float hitOffset = 0.09f;      // How much AI overshoots puck

    Rigidbody2D rb;
    public  Vector2 targetPos;
    public bool aiCanMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void FixedUpdate()
    {
        if( !aiCanMove) return;

        if (puck.position.y > minY)
        {
            ChaseAndHit();
        }
        else
        {
            Defend();
        }

        MoveSmooth();
    }

    // ================= logic for defend and following puck =================

    void ChaseAndHit()
    {
        float hitY = puck.position.y + hitOffset;

        targetPos = new Vector2(
            puck.position.x,
            Mathf.Clamp(hitY, minY, maxY)
        );
    }

    void Defend()
    {
        targetPos = new Vector2(
            Mathf.Lerp(0, 0f, 0.05f),
            Mathf.Lerp(rb.position.y, 2.79f, 0.05f)
        );
    }

    // ================= Movement =================

    void MoveSmooth()
    {
        Vector2 delta = targetPos - rb.position;
        float distance = delta.magnitude;

        // Slow down when very close (prevents hard smash)
        float speedMultiplier = distance < 0.5f ? 0.5f : 1f;

        float maxStep = speed * speedMultiplier * Time.fixedDeltaTime;
        Vector2 move = Vector2.ClampMagnitude(delta, maxStep);

        rb.MovePosition(rb.position + move);
    }
}
