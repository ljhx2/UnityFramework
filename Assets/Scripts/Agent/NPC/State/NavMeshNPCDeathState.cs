using UnityEngine;

public class NavMeshNPCDeathState : State
{
    private GameObject _agent;

    public NavMeshNPCDeathState(GameObject agent)
    {
        _agent = agent;
    }
    public override void Enter()
    {
        _agent.GetComponent<Collider>().enabled = false;
        _agent.GetComponent<Animator>().enabled = false;
        _agent.GetComponent<NavMeshEnemyAI>().IsDead = true;
    }

    public override void Exit()
    {
        return;
    }

    protected override void StateUpdate(float deltaTime)
    {
        return;
    }
}
