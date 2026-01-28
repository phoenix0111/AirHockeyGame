using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 30f;

    Rigidbody2D rb;
    Vector2 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void FixedUpdate()
    {
        if (!Input.GetMouseButton(0)) return;

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            targetPos,
            moveSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);
    }
}
