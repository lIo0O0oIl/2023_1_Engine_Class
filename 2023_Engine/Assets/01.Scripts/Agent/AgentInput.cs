using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using static Core.Define;

public class AgentInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnAttackKeyPress = null;    // ����
    public event Action OnRollingKeyPress = null; // �Ѹ�Ű ������ ��

    [SerializeField] private LayerMask whatIsGround;

    Vector3 directionInput;

    void Update()
    {
        UpdateMoveInput();
        UpdateAttackInput();    // ����
        UpdateRollingInput();
    }

    private void UpdateRollingInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnRollingKeyPress?.Invoke();
        }
    }

    private void UpdateAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackKeyPress?.Invoke();     // ����
        }
    }

    private void UpdateMoveInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // y�� ������
        float vertical = Input.GetAxisRaw("Vertical");  // z�� ������
        directionInput = new Vector3(horizontal, 0, vertical);
        OnMovementKeyPress?.Invoke(new Vector3(horizontal, 0, vertical));
    }

    public  Vector3 GetCurrentInputDirection()
    {
        Vector3 dir45 = Quaternion.Euler(0, -45f, 0) * directionInput.normalized;
        return dir45;
    }

    public Vector3 GetMouseWordPosition()
    {
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit, MainCam.farClipPlane, whatIsGround);

        if (result)
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

}
