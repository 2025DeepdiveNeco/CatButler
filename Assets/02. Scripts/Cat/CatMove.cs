using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMove : MonoBehaviour
{
    [Header("�̵� ����")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    [Header("���� ���� (�ð���)")]
    public Transform visualTransform;    // ĳ���� �̹����� ����ִ� �ڽ� ������Ʈ
    public SpriteRenderer playerVisualSr; // ĳ���� �̹����� SpriteRenderer
    public float jumpForce = 6f;         // ���� ��
    public float gravity = -16f;         // �������� �ӵ� (�߷�)
    public int defaultSortingOrder = 2;  // ��� ĳ���� ���̾� ����

    private float verticalVelocity;      // ���� ���� �ӵ�
    private float currentHeight;         // ���� ���� ����
    private bool isGrounded = true;      // �ٴ� ���� Ȯ��
    private Vector3 visualOriginalPos;    // �̹����� ó�� ���� ��ġ

    [Header("Water Step")]
    [SerializeField] float waterStepOffSetDistance = 2f;
    bool onWaterStep;
    Vector2 startWaterStep;

    public void OnWaterStep() => onWaterStep = true;

    void Awake()
    {
        // ������Ʈ �������� �� �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();

        // Rigidbody2D �⺻ ���� (ž���)
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        if (visualTransform != null)
        {
            visualOriginalPos = visualTransform.localPosition;

            // ���� SpriteRenderer�� �ν����Ϳ��� ���� ���ߴٸ� �ڵ����� ã��
            if (playerVisualSr == null)
                playerVisualSr = visualTransform.GetComponent<SpriteRenderer>();
        }

        startWaterStep = transform.position;
    }

    void Update()
    {
        // 1. �Է� ó�� (New Input System ���)
        HandleInput();

        // 2. ���� ó��
        ApplyJumpPhysics();

        // 3. ���̾� ���� ����
        // FadeObstacle ���� �ۿ� ���� ���� ����� �ٴڿ� ������ ���̾ �⺻������ ����
        if (!isGrounded)
        {
            if (playerVisualSr.sortingOrder <= defaultSortingOrder)
            {
                playerVisualSr.sortingOrder = defaultSortingOrder + 1;
            }
        }

        if(onWaterStep && isGrounded)
        {
            WaterSteop();
        }
    }

    void WaterSteop()
    {
        // TODO : Water Steop
        if (waterStepOffSetDistance 
            <= (startWaterStep - (Vector2)transform.position).magnitude)
            Instantiate(Resources.Load<GameObject>(""), transform);
    }

    void FixedUpdate()
    {
        // ���� ���� �̵� ó��
        rb.linearVelocity = moveInput * moveSpeed;
    }

    private void HandleInput()
    {
        // WASD �̵� �Է� (Keyboard.current ���)
        Vector2 input = Vector2.zero;
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.wKey.isPressed) input.y += 1;
        if (keyboard.sKey.isPressed) input.y -= 1;
        if (keyboard.aKey.isPressed) input.x -= 1;
        if (keyboard.dKey.isPressed) input.x += 1;

        moveInput = input.normalized;

        // ���� �Է� (Space)
        if (keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            StartJump();
        }
    }

    private void StartJump()
    {
        isGrounded = false;
        verticalVelocity = jumpForce;        
    }

    private void ApplyJumpPhysics()
    {
        if (isGrounded) return;

        // �߷� ���ӵ� ����
        verticalVelocity += gravity * Time.deltaTime;
        currentHeight += verticalVelocity * Time.deltaTime;

        // ���� ����
        if (currentHeight <= 0)
        {
            currentHeight = 0;
            verticalVelocity = 0;
            isGrounded = true;            
        }

        // 4. �ڽ�(�̹���) ������Ʈ�� Y�� ��ġ�� �����Ͽ� ����
        if (visualTransform != null)
        {
            visualTransform.localPosition = new Vector3(
                visualOriginalPos.x,
                visualOriginalPos.y + currentHeight,
                visualOriginalPos.z
            );
        }
    }

    // [�߿�] FadeObstacle���� ȣ���� �Լ�
    public bool IsJumping()
    {
        return !isGrounded;
    }

    // ���� ���̰� ���� (�ʿ�� ���)
    public float GetCurrentHeight()
    {
        return currentHeight;
    }
}