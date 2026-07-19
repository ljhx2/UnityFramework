using UnityEngine;

/// 상호 작용 가능한 개체를 사용자 지정 렌더링 기능을 사용하여 강조 표시하는 개요 기능
public class Highlight : MonoBehaviour
{
    [SerializeField] private Renderer _objectRenderer;
    [SerializeField] private string _outlineLayerName = "Outline";
    
    private int _originalLayer;

    private void Awake()
    {
        _originalLayer = _objectRenderer.gameObject.layer;
    }

    public void EnableHighlight()
    {
        _objectRenderer.gameObject.layer = LayerMask.NameToLayer(_outlineLayerName);
    }

    public void DisableHighlight()
    {
        _objectRenderer.gameObject.layer = _originalLayer;
    }
}
