using UnityEngine;

public class PlayerRotationStrategy : AgentRotationStrategy
{
    [SerializeField] GameObject _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    protected override float RotationStrategy(Vector3 inputDirection)
    {
        return Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
    }
}
