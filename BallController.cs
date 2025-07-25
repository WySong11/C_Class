using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // SerializeField
    // : public로 선언된 변수는 기본적으로 인스펙터에 노출됩니다.
    // : private/protected 필드를 Inspector에 노출하고 싶을 때 사용
    // : 캡슐링을 유지하면서 인스펙터에 노출시킬 수 있습니다.
    // : 에디터에서 값을 변경할 수 있지만, 코드에서는 private로 유지됩니다.
    [SerializeField]
    Rigidbody2D rb;

    // HideInInspector    
    // : HideInInspector를 사용하면 인스펙터에 노출되지 않습니다.
    // : 코드에서는 여전히 접근할 수 있습니다.
    [HideInInspector]
    public float speed = 5f;

    // HideInInspector와 SerializeField를 함께 사용하면
    // 인스펙터에 노출되지 않지만,
    // Unity가 값을 저장(직렬화)하고
    // 에디터에서 값을 변경할 수 없습니다.
    // 이 경우, 코드에서만 접근할 수 있습니다.
    [SerializeField, HideInInspector]
    private int hiddenValue = 10;

    // Range
    // : Range를 사용하면 인스펙터에서 슬라이더로 값을 조정할 수 있습니다.
    // : 슬라이더의 최소값과 최대값을 지정할 수 있습니다.
    [SerializeField, Range(0f, 100f)]
    private float health = 50f;

    // Tooltip
    // : Tooltip을 사용하면 인스펙터에서 변수에 대한 설명을 추가할 수 있습니다.
    [SerializeField, Tooltip("이 캐릭터의 최대 체력입니다.")]
    private int maxHealth = 100;

    // Header
    // : Header를 사용하면 인스펙터에서 변수 그룹에 제목을 추가할 수 있습니다.
    [SerializeField, Header("이동 관련 설정")]
    private float moveSpeed;

    [SerializeField]
    private float acceleration = 20f;    

    [SerializeField]
    private float MaxSpeed = 30.0f;


    [SerializeField, Header("점프 관련 설정")]
    public float MaxJump;

    // 바닥에 닿았는지 여부
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
        // 이동 입력 처리
        // : Unity의 입력 시스템을 사용하여
        // : 사용자의 입력을 처리하는 메서드입니다.
        // : "Horizontal"은 좌우 이동 입력을 처리합니다.
        // : "Vertical"은 상하 이동 입력을 처리합니다.

        // Input.GetAxisRaw
        // : Unity의 입력 시스템에서
        // : 입력 값을 즉시 반환하는 메서드입니다.
        // : Input.GetAxisRaw는 입력 값을 즉시 반환하므로
        // : 입력이 부드럽게 처리되지 않습니다.
        // : 예를 들어, 좌우 화살표 키나 A/D 키를 누르면
        // : -1, 0, 1의 값을 반환합니다.
        // : Input.GetAxisRaw는 프레임 레이트에 관계없이
        // : 입력 값을 즉시 반환하므로
        // : 입력이 부드럽게 처리되지 않습니다.
        //float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Input.GetAxis
        
        // : Input.GetAxis는 부드러운 입력 처리를 위해
        // : 입력 값을 보간합니다.
        // : 예를 들어, 좌우 화살표 키나 A/D 키를 누르면
        // : -1, 0, 1의 값을 반환합니다.
        // : Input.GetAxis는 프레임 레이트에 따라 입력 값을 보간하므로
        // : 입력이 부드럽게 처리됩니다.

        float moveX = Input.GetAxis("Horizontal");

        //float mooveZ = Input.GetAxisRaw("Mouse ScrollWheel");

        // Debug.Log
        // : Unity의 디버그 로그를 출력하는 메서드입니다.
        // : 주로 개발 중에 변수의 값을 확인하거나
        // : 코드의 흐름을 추적하는 데 사용됩니다.
        //Debug.Log("이동 입력: " + moveX + ", " + moveY);

        // 바닥에 닿았을 때만 이동 입력 처리
        if (isGrounded)
        {
            // AddForce
            // Rigidbody2D에 힘을 가하는 메서드입니다.
            // ForceMode2D.Force : 지속적인 힘을 가합니다.
            // 주로 물리적인 움직임을 구현할 때 사용됩니다.
            // AddForce는 Rigidbody2D의 질량을 고려하여 힘을 적용합니다.
            // AddForce는 물리 엔진에 의해 처리되므로
            // 프레임 레이트에 따라 힘의 적용이 달라질 수 있습니다.            

            //rb.AddForce(new Vector2(speed * moveX, 0f), ForceMode2D.Force);

            // Rigidbody2D.velocity
            // : Rigidbody2D의 속도를 직접 설정하는 프로퍼티입니다.
            // : Rigidbody2D.velocity를 사용하면
            // : 물리 엔진에 의해 처리되는 속도를 직접 설정할 수 있습니다.
            // : Rigidbody2D.velocity는 물리 엔진에 의해 처리되므로
            // : 프레임 레이트에 관계없이 속도를 설정할 수 있습니다.

            //rb.velocity = new Vector2(speed * moveX, 0f); // x축으로 초당 3의 속도

            // Rigidbody2D.MovePosition
            // : Rigidbody2D를 지정된 위치로 이동시키는 메서드입니다.
            // : Rigidbody2D.MovePosition을 사용하면
            // : 물리 엔진에 의해 처리되는 위치를 직접 설정할 수 있습니다.
            // : Rigidbody2D.MovePosition은 물리 엔진에 의해 처리되므로
            // : 프레임 레이트에 관계없이 위치를 설정할 수 있습니다.

            // 이동 방향 벡터 생성
            // Vector2는 2차원 벡터를 나타내는 구조체입니다.
            // Vector2는 Unity에서 2D 게임 개발에 사용되는 벡터입니다.
            // Vector2는 x축과 y축의 값을 가지며,
            // 벡터의 크기와 방향을 나타냅니다.
            // Vector2.normalized는 벡터의 크기를 1로 정규화합니다.
            // Vector2.normalized는 벡터의 방향을 유지하면서
            // 벡터의 크기를 1로 만듭니다.
            // 예를 들어, (3, 4) 벡터를 정규화하면
            // (0.6, 0.8) 벡터가 됩니다.
            
            Debug.Log("이동 입력: " + moveX + ", " + moveY);

            Vector2 moveDir = new Vector2(moveX, moveY).normalized;

            Debug.Log("이동 방향: " + moveDir);

            // magnitude 는 벡터 (x, y) 의 길이를 피타고라스의 정리로 계산한 값.
            // 방향 벡터의 크기(magnitude)가 0보다 크면, 즉 입력이 있으면 실행됩니다.
            // a² + b² = c²
            // Vector2.magnitude는 벡터의 크기를 반환합니다.

            if (moveDir.magnitude > 0)
            {
                Debug.Log("이동 방향 벡터: " + moveDir);

                // 가속도 적용
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, MaxSpeed);
            }
            else
            {
                // 입력이 없으면 속도 초기화
                currentSpeed = 0f;
            }

            rb.MovePosition(rb.position + moveDir * currentSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ForceMode2D.Impulse : 순간적인 힘을 가합니다.
            // 주로 점프나 충돌과 같은 순간적인 힘을 적용할 때 사용됩니다.
            // Impulse는 Rigidbody2D의 질량을 고려하여 힘을 적용합니다.
            // AddForce는 물리 엔진에 의해 처리되므로
            // 프레임 레이트에 관계없이 순간적인 힘을 적용합니다.

            rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        }
    }

    // 바닥에 닿았는지 판별 (Ground 태그 필요)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 바닥에 닿았을 때 isGrounded를 true로 설정
            isGrounded = true;

            Debug.Log("바닥에 닿았습니다.");
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
