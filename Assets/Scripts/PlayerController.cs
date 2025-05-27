using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public Animator animator;
    public TextMeshProUGUI coinsEarned;

    private Vector3 direction;
    public float forwardSpeed = 6f;
    private int currentLane = 1; // 0: left, 1: middle, 2: right
    private float laneDist = 2.75f;
    private float targetX = 0f;
    public float laneChangeSpeed = 25f;
    public float maxForwardSpeed = 15f;

    public float jumpForce = 10f;
    public float gravity = -20f;
    private bool isGrounded = true;
    private float verticalVelocity = 0f;

    public float slideDuration = 1f;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private float originalHeight;
    private float originalRadius;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        targetX = transform.position.x;

        originalHeight = characterController.height;
        originalRadius = characterController.radius;
    }

    void Update()
    {
        if (!GameManager.isTapped) return;

        // Speed up gradually
        if (forwardSpeed < maxForwardSpeed)
            forwardSpeed += 0.05f * Time.deltaTime;

        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        isGrounded = characterController.isGrounded;
        if (isGrounded && verticalVelocity < 0) verticalVelocity = -1f;

        if (isGrounded)
            animator.SetBool("isJumping", false);

        if (!isSliding && SwipeManager.swipeUp && isGrounded)
        {
            AudioManager audioManager = Object.FindFirstObjectByType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlaySound("Jump");
            }
            animator.SetBool("isSliding", false);
            verticalVelocity = jumpForce;
            animator.SetBool("isJumping", true);
        }

        if (SwipeManager.swipeDown && !isSliding)
        {
            StartSlide();
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            if (slideTimer <= 0f)
            {
                EndSlide();
            }
        }

        verticalVelocity += gravity * Time.deltaTime;
        direction.y = verticalVelocity;

        if (SwipeManager.swipeRight && currentLane < 2)
        {
            currentLane++;
            targetX = (currentLane - 1) * laneDist;
        }
        if (SwipeManager.swipeLeft && currentLane > 0)
        {
            currentLane--;
            targetX = (currentLane - 1) * laneDist;
        }

        float xDiff = targetX - transform.position.x;
        direction.x = xDiff * laneChangeSpeed;

        characterController.Move(direction * Time.deltaTime);
    }

    private void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        animator.SetBool("isSliding", true);

        characterController.radius = originalRadius / 2f;
        characterController.height = originalHeight / 2f;
        characterController.center = new Vector3(0f, -0.5f, 0f);
    }

    private void EndSlide()
    {
        isSliding = false;
        animator.SetBool("isSliding", false);

        characterController.radius = originalRadius;
        characterController.height = originalHeight;
        characterController.center = new Vector3(0f, 0f, 0f);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Obstacle")
        {
            GameManager.isGameOver = true;
            AudioManager audioManager = Object.FindFirstObjectByType<AudioManager>();
            if (audioManager != null)
            {
                audioManager.PlaySound("Game Over");
            }
        }
        coinsEarned.text = GameManager.noOfCoins.ToString();
    }
}