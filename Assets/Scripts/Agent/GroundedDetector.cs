using UnityEngine;

public class GroundedDetector : MonoBehaviour
{
    [field:SerializeField]
    public bool Grounded { get; private set; }
    [field: SerializeField]
    public bool StairsGrounded { get; private set; }

    [Header("Grounded Check")]
    [SerializeField] float _groundedOffset = 0.14f;
    [SerializeField] float _stairOffset = 0.02f;
    [SerializeField] float _groundedRadius = 0.28f;

    [SerializeField] LayerMask _groundLayers;

    public void GroundedCheck()
    {
        Grounded = GroundedCheck(_groundedOffset);
        StairsGrounded = GroundedCheck(_stairOffset);
    }

    private bool GroundedCheck(float groundedOffset)
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
        return Physics.CheckSphere(spherePosition, _groundedRadius, _groundLayers, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0f, 1f, 0f, 0.35f);
        Color transparentRed = new Color(1f, 0f, 0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + _groundedOffset, transform.position.z), _groundedRadius);
    }
}
