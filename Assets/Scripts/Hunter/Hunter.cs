using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hunter : MonoBehaviour
{
    [SerializeField] float _speedMov;
    [SerializeField] float _speedReg;
    [SerializeField] float _energy;
    FSM<string> _fsm;
    [SerializeField] Vector3 _velocity;
    [SerializeField] float _maxVelocity;
    [SerializeField] float _radiusPersuit;
    [SerializeField] float _radiusAttack;
    [SerializeField] float _maxForce;
    [SerializeField] float _prediction;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _firePoint;
    [SerializeField] private float _shootCooldown = 1f; 
    private float _lastShootTime = -Mathf.Infinity;

    [SerializeField] private Transform[] _waypoints;


    public Vector3 Velocity => _velocity;
   


    private void Awake()
    {

        _fsm = new FSM<string>();

        _fsm.AddState(HunterStatesNames.Idle, new IdleState(
             _fsm,
             () => _energy,
             (value) => _energy = value,
             _energy,
             _speedReg
         ));
        _fsm.AddState(HunterStatesNames.Movement, new MovementState(
            _fsm,
            transform,
            _radiusPersuit,
            _waypoints,
            (targetPos) => Seek(targetPos),
            (force) => AddForce(force),
             () => _energy,
            (value) => _energy = value
        ));
        _fsm.AddState(HunterStatesNames.Hunting, new HuntingState(
            _fsm, 
            transform,
            _radiusPersuit, 
            _radiusAttack,
            (boid) => Pursuit(boid), 
            (force) => AddForce(force),
            (boid) => ShootAtBoid(boid)
            )
        );

        _fsm.ChangeState(HunterStatesNames.Movement);
    }

    private void Update()
    {
        _fsm.ArtificialUpdate();   
        transform.position += _velocity * Time.deltaTime;
    }

    public void AddForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);

        if (_velocity.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(_velocity.x, 0, _velocity.z);
            if (direction.sqrMagnitude > 0.01f)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public Vector3 Seek(Vector3 target)
    {
        var desired = target - transform.position;
        desired.Normalize(); 
        desired *= _maxVelocity; 

        var steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce);


        return steering;
    }

    public Vector3 Pursuit(Boid target)
    {
        var desired = target.transform.position + (target.GetVelocity() * _prediction);

        return Seek(desired);
    }

    public void ShootAtBoid(Boid target)
    {
        if (Time.time - _lastShootTime < _shootCooldown)
            return; 

        _lastShootTime = Time.time;

        var predictedPos = target.transform.position + target.GetVelocity() * _prediction;
        var direction = predictedPos - transform.position;

        var bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.LookRotation(direction));
        bullet.GetComponent<Bullet>().Init(direction);
    }
}

public static class HunterStatesNames
{
    public static string Idle = "Idle";
    public static string Movement = "Movement";
    public static string Hunting = "Hunting";
}