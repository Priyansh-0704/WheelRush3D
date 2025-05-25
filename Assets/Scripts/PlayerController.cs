using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 direction;
    public float forwardSpeed = 5f;
    private int currentLane = 1; // 0: left, 1: middle, 2: right
    private float laneDist = 2.75f;
    private float targetX = 0f;
    public float laneChangeSpeed = 25f;

    public float jumpForce = 10f;
    public float gravity = -20f;
    private bool isGrounded = true;
    private float verticalVelocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        targetX = transform.position.x;
    }

    void Update()
    {
        direction.z = forwardSpeed;

        isGrounded = characterController.isGrounded;

        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -1f; // Small downward force to keep grounded

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpForce;
        }

        verticalVelocity += gravity * Time.deltaTime;
        direction.y = verticalVelocity;

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
        {
            currentLane++;
            targetX = (currentLane - 1) * laneDist;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
        {
            currentLane--;
            targetX = (currentLane - 1) * laneDist;
        }

        float xDiff = targetX - transform.position.x;
        direction.x = xDiff * laneChangeSpeed;

        characterController.Move(direction * Time.deltaTime);
    }
}