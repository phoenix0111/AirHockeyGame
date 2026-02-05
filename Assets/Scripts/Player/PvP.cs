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

    int fingerId = -1;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void Update()
    {
        canMove = puck.canPlayerMove;
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

    // ---------------- MOBILE INPUT (FIXED) ----------------
    void HandleMobileInput()
    {
        foreach (Touch t in Input.touches)
        {
            // Assign finger
            if (fingerId == -1 && t.phase == TouchPhase.Began)
            {
                if (isPlayer1 && t.position.y < Screen.height / 2)
                    fingerId = t.fingerId;

                if (!isPlayer1 && t.position.y > Screen.height / 2)
                    fingerId = t.fingerId;
            }

            // Move only with assigned finger
            if (t.fingerId == fingerId)
            {
                if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
                {
                    SetTarget(t.position);
                }

                if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
                {
                    fingerId = -1;
                }
            }
        }
    }

    void SetTarget(Vector2 screenPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        targetPos = worldPos;
    }
}
