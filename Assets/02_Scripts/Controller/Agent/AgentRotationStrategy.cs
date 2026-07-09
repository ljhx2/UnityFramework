using UnityEngine;

/// <summary>
/// 캐릭터 회전 방식을 정의하는 추상 클래스
/// </summary>
public abstract class AgentRotationStrategy : MonoBehaviour
{
    public float RotationCalculation(Vector2 movementInput, Transform agentTransform, ref float rotationVelocity, float RotationSmoothTime, float targetRotation)
    {
        Vector3 inputDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        if (movementInput != Vector2.zero)
        {
            targetRotation = RotationStrategy(inputDirection);
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                RotationSmoothTime);

            agentTransform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }
        return targetRotation;
    }

    protected abstract float RotationStrategy(Vector3 inputDirection);
}
