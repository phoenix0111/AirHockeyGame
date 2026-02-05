using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;
    Rigidbody2D rb;
    public Puck puck;


    public Vector2 targetPos;
    public bool canMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void Update()
    {
        canMove = puck.canPlayerMove;

        if (Input.GetMouseButton(0) && canMove)
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        targetPos.y = Mathf.Clamp(targetPos.y, -4f, -0.83f);  // blocks player movement on half side of board
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
    }

}
