using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera")]
    public GameObject CinemachineCameraTarget;
    public float TopCameraLimit = 70f;
    public float BottomCameraLimit = -30f;

    private GameObject _mainCamera;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _cameraRotationThreshold = 0.01f;

    PlayerGameInput _input;

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _input = GetComponent<PlayerGameInput>();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        if (_input.CameraInput.sqrMagnitude >= _cameraRotationThreshold)
        {
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += _input.CameraInput.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.CameraInput.y * deltaTimeMultiplier;
        }

        //회전 범위를 제한하여 값이 360도로 제한
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomCameraLimit, TopCameraLimit);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
