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
    public float RoamRange;
    public float PushForce;

    private State _currentState = State.IDLE;
    private Vector3 _target;
    private Rigidbody _rigidbody;

    private List<float> _distances = new List<float>();
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();

        _target = new Vector3();
        // Code reuse metric hey ho
        _target.x = Random.Range(_startPosition.x - RoamRange, _startPosition.x + RoamRange);
        _target.y = _startPosition.y;
        _target.z = Random.Range(_startPosition.z - RoamRange, _startPosition.z + RoamRange);
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

        else if (_currentState == State.IDLE)
        {
            if (transform.position == _target)
            {
                _target.x = Random.Range(_startPosition.x - RoamRange, _startPosition.x + RoamRange);
                _target.y = _startPosition.y;
                _target.z = Random.Range(_startPosition.z - RoamRange, _startPosition.z + RoamRange);
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

        else if (_currentState == State.IDLE)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, Speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody playerBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 toCollision = collision.gameObject.transform.position - transform.position;
            playerBody.AddForce(toCollision * PushForce);
        }
    }
}
