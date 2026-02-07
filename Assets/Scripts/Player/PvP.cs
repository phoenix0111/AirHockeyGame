using UnityEngine;

public class PvP : MonoBehaviour
{
    public Puck puck;
    public float speed = 17f;
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
    }

    void FixedUpdate()
    {
        if (!canMove) return;

#if UNITY_ANDROID
         HandleMobileInput();
#else 
         HandleKeybaodandWEBGLMobile_Input();
#endif

        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            targetPos,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);
    }


    void HandleKeybaodandWEBGLMobile_Input()
    {

        Vector2 input = Vector2.zero;

        if (isPlayer1)
        {
            input.x = (Input.GetKey(KeyCode.D) ? 1 : 0) -
                      (Input.GetKey(KeyCode.A) ? 1 : 0);

            input.y = (Input.GetKey(KeyCode.W) ? 1 : 0) -
                      (Input.GetKey(KeyCode.S) ? 1 : 0);

        }
        else
        {
            input.x = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) -
                      (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);

            input.y = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) -
                      (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);

        }

        // Normalize so diagonal isn't faster
        if (input.sqrMagnitude > 1)
            input.Normalize();

        targetPos = rb.position + input * speed * Time.deltaTime;

        foreach (Touch t in Input.touches)
        {
            if (isPlayer1 && t.position.y < Screen.height / 2)
                SetTarget(t.position);

            if (!isPlayer1 && t.position.y > Screen.height / 2)
                SetTarget(t.position);
        }



    }

   
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
