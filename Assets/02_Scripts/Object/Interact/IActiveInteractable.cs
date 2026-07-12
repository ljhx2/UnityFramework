
//활성화하거나 비활성화할 수 있는 Interactable
public interface IActiveInteractable : IInteractable
{
    bool IsInteractionActive { get; }
}
