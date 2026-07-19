using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;
    //Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    //Dictionary<int, GameObject> _env = new Dictionary<int, GameObject>();

    public GameObject GetPlayer() { return _player; }
    public void SpawnAsync(Define.WorldObject type, string key, Transform parent = null, Action<GameObject> completed = null)
    {
        //GameObject go = Managers.Resource.Instantiate(path, parent);
        Managers.ResourceA.InstantiateAsync(key, parent, (go) =>
        {
            switch (type)
            {
                case Define.WorldObject.Player:
                    _player = go;
                    break;
                case Define.WorldObject.Monster:
                    _monsters.Add(go);
                    break;
            }
            completed?.Invoke(go);
        });
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                        _monsters.Remove(go);
                }
                break;
        }

        Managers.ResourceA.Destroy(go);
    }
}
