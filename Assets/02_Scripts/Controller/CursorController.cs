using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    Texture2D _attackIcon = null;
    Texture2D _handIcon = null;

    CursorType _cursorType = CursorType.None;

    int _clickMask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    void Start()
    {
        Managers.ResourceA.LoadAsync<Texture2D>("Textures/Cursor/Attack", (tex) =>
        {
            _attackIcon = tex;
        });
        Managers.ResourceA.LoadAsync<Texture2D>("Textures/Cursor/Hand", (tex) =>
        {
            _handIcon = tex;
        });
    }

    void Update()
    {
        if (_attackIcon == null || _handIcon == null)
            return;

        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, _clickMask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5f, 0f), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3f, 0f), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }

            }
        }
    }
}
