using UnityEngine;

public class PvP : MonoBehaviour
{
    public Puck puck;
    public float speed = 15f;
    Rigidbody2D rb;

    public Vector2 targetPos;
    public bool canMove = true;

    [Header("Player Settings")]
    public bool isPlayer1;   

    [Header("Movement Limits")]
    public float minY;
    public float maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void Update()
    {
        canMove = puck.canPlayerMove ;

        if (!canMove) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        HandlePCInput();
#else
        HandleMobileInput();
#endif

        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            targetPos,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);
    }

    // ---------------- PC INPUT ----------------
    void HandlePCInput()
    {
        if (isPlayer1 && Input.GetMouseButton(0))
            SetTarget(Input.mousePosition);

        if (!isPlayer1 && Input.GetMouseButton(1))
            SetTarget(Input.mousePosition);
    }

    // ---------------- MOBILE INPUT ----------------
    void HandleMobileInput()
    {
        foreach (Touch t in Input.touches)
        {
            if (isPlayer1 && t.position.y < Screen.height / 2)
                SetTarget(t.position);

            if (!isPlayer1 && t.position.y > Screen.height / 2)
                SetTarget(t.position);
        }
    }

    void SetTarget(Vector2 screenPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        targetPos = worldPos;
    }
}
