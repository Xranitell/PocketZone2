
using System;
using NaughtyAttributes;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Enemy : Entity, ITargetable, IWatcher
{
    [BoxGroup("Components")] [SerializeField] private GameObject targetMark;
    [BoxGroup("Components")] public Rigidbody2D rigidbody;
    [BoxGroup("Components")] public SearchTargets searchTargets;

    [Header("Stats")]
    public float spawnPriority = 50;
    public float attackDistance = 0.5f;
    public float seeDistance = 2f;
    public float speed = 2f;
    

    [BoxGroup("StateMachine")] private EnemyState currentState;
    [BoxGroup("StateMachine")] private EnemyState startState;
    [BoxGroup("StateMachine")][SerializeField] private AttackEnemyState attackEnemyState;
    [BoxGroup("StateMachine")][SerializeField] private FollowEnemyState followEnemyState;
    [BoxGroup("StateMachine")][SerializeField] private IdleEnemyState idleEnemyState;
    
    public float SeeDistance => seeDistance;

    protected override void Awake()
    {
        base.Awake();
        
        attackEnemyState = new AttackEnemyState();
        followEnemyState = new FollowEnemyState();
        idleEnemyState = new IdleEnemyState();
        startState = idleEnemyState;
    }

    private void Start()
    {
        SetState(startState);
    }

    private void Update()
    {
        if (!currentState.isFinished)
        {
            currentState.Run();
        }
        else
        {
            if (searchTargets.currentTarget == null)
                SetState(idleEnemyState);
            else if (Vector3.Distance(transform.position, searchTargets.currentTarget.transform.position) <= attackDistance)
                SetState(attackEnemyState);
            else 
                SetState(followEnemyState);
        }
    }

    private void SetState(EnemyState state)
    {
        currentState = state;
        currentState.character = this;
        currentState.Init();
    }


    public void ChooseAsTarget()
    {
        targetMark.SetActive(true);
    }
    public void UnTarget()
    {
        targetMark.SetActive(false);
    }
    
    private void OnDisable()
    {
        EnemySpawner.enemyPull.PushToPull(this);
        
    }
    public void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        Flip((transform.position - target).normalized.x);
    }

    private void Flip(float xValue)
    {
        Quaternion rot = transform.rotation;
        rot.y = xValue>0 ? 180: 0;
        transform.rotation = rot;
    }

    
}

