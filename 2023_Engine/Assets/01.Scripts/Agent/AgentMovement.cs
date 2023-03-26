using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public bool IsActiveMove = true;    // 키보드로 이동하냐? 아니냐

    [SerializeField]
    private float moveSpeed = 8f, gravity = -9.8f;
    
    private CharacterController characterController;

    private Vector3 movementVelocity;
    public Vector3 MovementVelocity => movementVelocity;
    private float verticalVelocity;

    private AgentAnimator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = transform.Find("Visual").GetComponent<AgentAnimator>();
    }

    public void SetMovementVelocity(Vector3 value)
    {
        movementVelocity = value;
    }

    private void CalculatePlayerMovement()
    {
        // 여기가 핵심
        animator?.SetSpeed(movementVelocity.sqrMagnitude);  // 추가됨

        movementVelocity.Normalize();

        movementVelocity *= moveSpeed *Time.fixedDeltaTime;
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;

        if (movementVelocity.sqrMagnitude > 0) // 길이구하기? 피타고라스의 법칙에서 빗변 구하는거
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
            // 가야할 방향 보게 하기
        }

    }

    public void Stoplmmediately()
    {
        movementVelocity = Vector3.zero;
        animator?.SetSpeed(movementVelocity.sqrMagnitude);  // 추가됨
    }

    public void SetRotation(Vector3 targetPos)  // 지점을 바라보는 코드
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void FixedUpdate() 
    {
        if (IsActiveMove)
        {
            CalculatePlayerMovement();  // 플레이어 이속 계산 (키보드 입력시에만 45도 계산)
        }

        // 중력에 따른 계산
        if (characterController.isGrounded == false)
        {
            verticalVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            // 0.3 은 하드코딩된 값
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);

        animator?.SetAirbone(characterController.isGrounded == false);  // 추가됨
    }
}
