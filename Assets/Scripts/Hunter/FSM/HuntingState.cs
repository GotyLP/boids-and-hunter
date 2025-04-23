using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UIElements;
using System;

public class HuntingState : IState
{
    FSM<string> _fsm;
    private float _radiusPersuit;
    private float _radiusAttack;
    Transform _transform;
    private Boid _currentTarget;

    private Func<Boid, Vector3> _pursuitFunc;
    private Action<Vector3> _addForceFunc;
    private Action<Boid> _attackFunc;

    public HuntingState(FSM<string> fsm, Transform transform, float radiusPersuit, float radiusAttack, Func<Boid, Vector3> Persuit, Action<Vector3> addForce, Action<Boid> attackFunc)
    {
        _fsm = fsm;
        _transform = transform;
        _radiusPersuit = radiusPersuit;
        _radiusAttack = radiusAttack;
        _pursuitFunc = Persuit;
        _addForceFunc = addForce;
        _attackFunc = attackFunc;

    }
    public void OnEnter()
    {
        Debug.Log("On Enter HuntingState");
        _currentTarget = null;
    }

    public void OnExit()
    {
        Debug.Log("On Exit HuntingState");
        _currentTarget = null;
    }

    private void FindTarget(List<Boid> boids)
    {
        foreach (Boid boid in boids)
        {
            if (boid == null) return;
            float dist = Vector3.Distance(boid.transform.position, _transform.position);

            if (dist < _radiusAttack || dist < _radiusPersuit)
            {
                _currentTarget = boid;
                Debug.Log("Nuevo objetivo");
                return;
            }
        }

        _fsm.ChangeState(HunterStatesNames.Movement);
    }


    public void PersuitBoid(Boid boid)
    {
        Vector3 steering = _pursuitFunc(boid);
        _addForceFunc?.Invoke(steering);
    }

    public void AttackBoid(Boid boid)
    {
        _attackFunc?.Invoke(boid);
    }

    public void OnUpdate()
    {
        Debug.Log("On Update HuntingState");
        if (_currentTarget != null)
        {
            float dist = Vector3.Distance(_currentTarget.transform.position, _transform.position);

            if (dist < _radiusAttack)
            {
                AttackBoid(_currentTarget);
            }
            else if (dist < _radiusPersuit)
            {
                PersuitBoid(_currentTarget);
            }
            else
            {
                _currentTarget = null;
                _fsm.ChangeState(HunterStatesNames.Movement);
            }
        }
        else
        {
            FindTarget(GameManager.Instance.totalBoids);
        }
    }
}
