using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    IDLE, ATTACK
}

public class JumperAI : MonoBehaviour
{
    public Transform[] Players;
    public float AgroRange;
    public float Speed;

    private State _currentState = State.IDLE;
    private Vector3 _target;
    private Rigidbody _rigidbody;

    private List<float> _distances = new List<float>();

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _distances.Clear();

        for (int i = 0; i < Players.Length; i++)
        {
            _distances.Add(Vector3.Distance(Players[i].position, transform.position));
        }

        float currentDistance = 1000.0f;

        for (int j = 0; j < _distances.Count; j++)
        {
            if (_distances[j] <= AgroRange && _distances[j] < currentDistance)
            {
                _target = Players[j].position;
                currentDistance = _distances[j];

                _currentState = State.ATTACK;
            }
        }

        if (_currentState == State.ATTACK)
        {
            if (Vector3.Distance(_target, transform.position) > AgroRange)
            {
                _currentState = State.IDLE;
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }

    void FixedUpdate()
    {
        if (_currentState == State.ATTACK && _target.y <= transform.position.y)
        {
            Vector3 toTarget = _target - transform.position;
            _rigidbody.AddForce(toTarget * Speed);
        }
    }
}
