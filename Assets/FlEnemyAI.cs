using System.Collections;
using Common;
using Pathfinding;
using PlayerScripts;
using UnityEngine;

public class FlEnemyAI : EnemyAI
{
    [SerializeField] private Seeker _seeker;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Path _path;
    private int _currentWayPoint = 0;
    private int _nextWayPointDistance = 3;
    private bool _reachedEndOfPath = false;
    private float _speed = 200;
    public override void Construct(Player player)
    {
        Player = player;
        StateMachine = new StateMachine();
        StartCoroutine(UpdatePath());
    }

    protected override void Update()
    {
        StateMachine.Update();
    }

    protected override void FixedUpdate()
    {
        if(_path == null) return;
        _reachedEndOfPath = _currentWayPoint >= _path.vectorPath.Count;

        Vector2 direction = (_path.vectorPath[_currentWayPoint] - transform.position).normalized;
        Vector2 force = direction * _speed * Time.deltaTime;
        _rigidbody.AddForce(force);
        float distance = Vector2.Distance(transform.position, _path.vectorPath[_currentWayPoint]);

        if (distance < _nextWayPointDistance)
        {
            _currentWayPoint++;
        }
        
    }

    private IEnumerator UpdatePath()
    {
        yield return new WaitForSeconds(.5f);
        BuildPath();
    }

    private void BuildPath()
    {
        if (_seeker.IsDone())
        {
            Vector3 playerPosition = Player.transform.position + Vector3.up * 2;
            _seeker.StartPath(transform.position, playerPosition, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWayPoint = 0;
        }
    }
}