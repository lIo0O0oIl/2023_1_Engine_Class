using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public bool IsActiveMove = true;    // Ű����� �̵��ϳ�? �ƴϳ�

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
        // ���Ⱑ �ٽ�
        animator?.SetSpeed(movementVelocity.sqrMagnitude);  // �߰���

        movementVelocity.Normalize();

        movementVelocity *= moveSpeed *Time.fixedDeltaTime;
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;

        if (movementVelocity.sqrMagnitude > 0) // ���̱��ϱ�? ��Ÿ����� ��Ģ���� ���� ���ϴ°�
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
            // ������ ���� ���� �ϱ�
        }

    }

    public void Stoplmmediately()
    {
        movementVelocity = Vector3.zero;
        animator?.SetSpeed(movementVelocity.sqrMagnitude);  // �߰���
    }

    public void SetRotation(Vector3 targetPos)  // ������ �ٶ󺸴� �ڵ�
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void FixedUpdate() 
    {
        if (IsActiveMove)
        {
            CalculatePlayerMovement();  // �÷��̾� �̼� ��� (Ű���� �Է½ÿ��� 45�� ���)
        }

        // �߷¿� ���� ���
        if (characterController.isGrounded == false)
        {
            verticalVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            // 0.3 �� �ϵ��ڵ��� ��
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);

        animator?.SetAirbone(characterController.isGrounded == false);  // �߰���
    }
}
