using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10f;

    [SerializeField]
    float _attackRange = 2f;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();
    }

    protected override void UpdateDie() 
    {
        Debug.Log("Monster UpdateDie");
    }
    protected override void UpdateMoving() 
    {
        Debug.Log("Monster UpdateMoving");
    }
    protected override void UpdateIdle() 
    {
        Debug.Log("Monster UpdateIdle");
    }
    protected override void UpdateSkill() 
    {
        Debug.Log("Monster UpdateSkill");
    }

    void OnHitEvent()
    {
        Debug.Log("Monster OnHitEvent");
    }
}
