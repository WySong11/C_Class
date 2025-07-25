using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // SerializeField
    // : public�� ����� ������ �⺻������ �ν����Ϳ� ����˴ϴ�.
    // : private/protected �ʵ带 Inspector�� �����ϰ� ���� �� ���
    // : ĸ������ �����ϸ鼭 �ν����Ϳ� �����ų �� �ֽ��ϴ�.
    // : �����Ϳ��� ���� ������ �� ������, �ڵ忡���� private�� �����˴ϴ�.
    [SerializeField]
    Rigidbody2D rb;

    // HideInInspector    
    // : HideInInspector�� ����ϸ� �ν����Ϳ� ������� �ʽ��ϴ�.
    // : �ڵ忡���� ������ ������ �� �ֽ��ϴ�.
    [HideInInspector]
    public float speed = 5f;

    // HideInInspector�� SerializeField�� �Բ� ����ϸ�
    // �ν����Ϳ� ������� ������,
    // Unity�� ���� ����(����ȭ)�ϰ�
    // �����Ϳ��� ���� ������ �� �����ϴ�.
    // �� ���, �ڵ忡���� ������ �� �ֽ��ϴ�.
    [SerializeField, HideInInspector]
    private int hiddenValue = 10;

    // Range
    // : Range�� ����ϸ� �ν����Ϳ��� �����̴��� ���� ������ �� �ֽ��ϴ�.
    // : �����̴��� �ּҰ��� �ִ밪�� ������ �� �ֽ��ϴ�.
    [SerializeField, Range(0f, 100f)]
    private float health = 50f;

    // Tooltip
    // : Tooltip�� ����ϸ� �ν����Ϳ��� ������ ���� ������ �߰��� �� �ֽ��ϴ�.
    [SerializeField, Tooltip("�� ĳ������ �ִ� ü���Դϴ�.")]
    private int maxHealth = 100;

    // Header
    // : Header�� ����ϸ� �ν����Ϳ��� ���� �׷쿡 ������ �߰��� �� �ֽ��ϴ�.
    [SerializeField, Header("�̵� ���� ����")]
    private float moveSpeed;

    [SerializeField]
    private float acceleration = 20f;    

    [SerializeField]
    private float MaxSpeed = 30.0f;


    [SerializeField, Header("���� ���� ����")]
    public float MaxJump;

    // �ٴڿ� ��Ҵ��� ����
    private bool isGrounded = false;

    private float currentSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
      //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �̵� �Է� ó��
        // : Unity�� �Է� �ý����� ����Ͽ�
        // : ������� �Է��� ó���ϴ� �޼����Դϴ�.
        // : "Horizontal"�� �¿� �̵� �Է��� ó���մϴ�.
        // : "Vertical"�� ���� �̵� �Է��� ó���մϴ�.

        // Input.GetAxisRaw
        // : Unity�� �Է� �ý��ۿ���
        // : �Է� ���� ��� ��ȯ�ϴ� �޼����Դϴ�.
        // : Input.GetAxisRaw�� �Է� ���� ��� ��ȯ�ϹǷ�
        // : �Է��� �ε巴�� ó������ �ʽ��ϴ�.
        // : ���� ���, �¿� ȭ��ǥ Ű�� A/D Ű�� ������
        // : -1, 0, 1�� ���� ��ȯ�մϴ�.
        // : Input.GetAxisRaw�� ������ ����Ʈ�� �������
        // : �Է� ���� ��� ��ȯ�ϹǷ�
        // : �Է��� �ε巴�� ó������ �ʽ��ϴ�.
        //float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Input.GetAxis
        
        // : Input.GetAxis�� �ε巯�� �Է� ó���� ����
        // : �Է� ���� �����մϴ�.
        // : ���� ���, �¿� ȭ��ǥ Ű�� A/D Ű�� ������
        // : -1, 0, 1�� ���� ��ȯ�մϴ�.
        // : Input.GetAxis�� ������ ����Ʈ�� ���� �Է� ���� �����ϹǷ�
        // : �Է��� �ε巴�� ó���˴ϴ�.

        float moveX = Input.GetAxis("Horizontal");

        //float mooveZ = Input.GetAxisRaw("Mouse ScrollWheel");

        // Debug.Log
        // : Unity�� ����� �α׸� ����ϴ� �޼����Դϴ�.
        // : �ַ� ���� �߿� ������ ���� Ȯ���ϰų�
        // : �ڵ��� �帧�� �����ϴ� �� ���˴ϴ�.
        //Debug.Log("�̵� �Է�: " + moveX + ", " + moveY);

        // �ٴڿ� ����� ���� �̵� �Է� ó��
        if (isGrounded)
        {
            // AddForce
            // Rigidbody2D�� ���� ���ϴ� �޼����Դϴ�.
            // ForceMode2D.Force : �������� ���� ���մϴ�.
            // �ַ� �������� �������� ������ �� ���˴ϴ�.
            // AddForce�� Rigidbody2D�� ������ ����Ͽ� ���� �����մϴ�.
            // AddForce�� ���� ������ ���� ó���ǹǷ�
            // ������ ����Ʈ�� ���� ���� ������ �޶��� �� �ֽ��ϴ�.            

            //rb.AddForce(new Vector2(speed * moveX, 0f), ForceMode2D.Force);

            // Rigidbody2D.velocity
            // : Rigidbody2D�� �ӵ��� ���� �����ϴ� ������Ƽ�Դϴ�.
            // : Rigidbody2D.velocity�� ����ϸ�
            // : ���� ������ ���� ó���Ǵ� �ӵ��� ���� ������ �� �ֽ��ϴ�.
            // : Rigidbody2D.velocity�� ���� ������ ���� ó���ǹǷ�
            // : ������ ����Ʈ�� ������� �ӵ��� ������ �� �ֽ��ϴ�.

            //rb.velocity = new Vector2(speed * moveX, 0f); // x������ �ʴ� 3�� �ӵ�

            // Rigidbody2D.MovePosition
            // : Rigidbody2D�� ������ ��ġ�� �̵���Ű�� �޼����Դϴ�.
            // : Rigidbody2D.MovePosition�� ����ϸ�
            // : ���� ������ ���� ó���Ǵ� ��ġ�� ���� ������ �� �ֽ��ϴ�.
            // : Rigidbody2D.MovePosition�� ���� ������ ���� ó���ǹǷ�
            // : ������ ����Ʈ�� ������� ��ġ�� ������ �� �ֽ��ϴ�.

            // �̵� ���� ���� ����
            // Vector2�� 2���� ���͸� ��Ÿ���� ����ü�Դϴ�.
            // Vector2�� Unity���� 2D ���� ���߿� ���Ǵ� �����Դϴ�.
            // Vector2�� x��� y���� ���� ������,
            // ������ ũ��� ������ ��Ÿ���ϴ�.
            // Vector2.normalized�� ������ ũ�⸦ 1�� ����ȭ�մϴ�.
            // Vector2.normalized�� ������ ������ �����ϸ鼭
            // ������ ũ�⸦ 1�� ����ϴ�.
            // ���� ���, (3, 4) ���͸� ����ȭ�ϸ�
            // (0.6, 0.8) ���Ͱ� �˴ϴ�.
            
            Debug.Log("�̵� �Է�: " + moveX + ", " + moveY);

            Vector2 moveDir = new Vector2(moveX, moveY).normalized;

            Debug.Log("�̵� ����: " + moveDir);

            // magnitude �� ���� (x, y) �� ���̸� ��Ÿ����� ������ ����� ��.
            // ���� ������ ũ��(magnitude)�� 0���� ũ��, �� �Է��� ������ ����˴ϴ�.
            // a�� + b�� = c��
            // Vector2.magnitude�� ������ ũ�⸦ ��ȯ�մϴ�.

            if (moveDir.magnitude > 0)
            {
                Debug.Log("�̵� ���� ����: " + moveDir);

                // ���ӵ� ����
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, MaxSpeed);
            }
            else
            {
                // �Է��� ������ �ӵ� �ʱ�ȭ
                currentSpeed = 0f;
            }

            rb.MovePosition(rb.position + moveDir * currentSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ForceMode2D.Impulse : �������� ���� ���մϴ�.
            // �ַ� ������ �浹�� ���� �������� ���� ������ �� ���˴ϴ�.
            // Impulse�� Rigidbody2D�� ������ ����Ͽ� ���� �����մϴ�.
            // AddForce�� ���� ������ ���� ó���ǹǷ�
            // ������ ����Ʈ�� ������� �������� ���� �����մϴ�.

            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
    }

    // �ٴڿ� ��Ҵ��� �Ǻ� (Ground �±� �ʿ�)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // �ٴڿ� ����� �� isGrounded�� true�� ����
            isGrounded = true;

            Debug.Log("�ٴڿ� ��ҽ��ϴ�.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
