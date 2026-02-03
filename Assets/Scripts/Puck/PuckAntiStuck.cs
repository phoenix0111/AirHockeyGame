using UnityEngine;

public class PuckAntiStuck : MonoBehaviour
{
    Rigidbody2D rb;
    public Puck puck;

    float stuckTimer;
    float stuckVelocityThreshold = 0.1f;
    float stuckTimeLimit = 0.25f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If puck is barely moving
        if (puck != null)
        {
            if (rb.linearVelocity.magnitude < stuckVelocityThreshold & puck.canPlayerMove)
            {
                stuckTimer += Time.deltaTime;

                if (stuckTimer >= stuckTimeLimit)
                {
                    UnstuckPuck();
                    stuckTimer = 0f;
                }
            }
            else
            {
                stuckTimer = 0f;
            }
        }
    }

    void UnstuckPuck()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(randomDir * 3f, ForceMode2D.Impulse);
    }
}
