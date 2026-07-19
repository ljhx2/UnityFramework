using UnityEngine;

public class SwitchInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationTriggerName = "Activate";

    private bool _isSwithched = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Interact(GameObject interactor)
    {
        if (_isSwithched == true)
            return;
        _isSwithched = true;
        _animator.SetTrigger(_animationTriggerName);
    }
}
