
using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity, ITargetable, IWatcher
{
    [BoxGroup("Components")] [SerializeField] private GameObject targetMark;
    [BoxGroup("Components")] public SearchTargets searchTargets;

    [Header("Stats")]
    public float spawnPriority = 50;
    public float seeDistance = 2f;
    public float speed = 2f;
    
    [BoxGroup("Attack state")]public float attackDistance = 0.5f;
    [BoxGroup("Attack state")]public float stopDistance = 0.3f;
    [BoxGroup("Attack state")]public float delayAttack = 0.2f;
    [BoxGroup("Attack state")]public float damageAttack = 5f;
    [BoxGroup("Attack state")]public Vector2 attackCenterOffset = new Vector2(0, 1f);
    
    [BoxGroup("StateMachine")] public EnemyState _currentState;
    [BoxGroup("StateMachine")] public EnemyState _startState;
    
    [BoxGroup("StateMachine")][SerializeField] private AttackEnemyState attackEnemyState;
    [BoxGroup("StateMachine")][SerializeField] private FollowEnemyState followEnemyState;
    [BoxGroup("StateMachine")][SerializeField] private IdleEnemyState idleEnemyState;
    
    [Expandable]public List<Item> loot = new List<Item>();
    public float SeeDistance => seeDistance;

    protected void Awake()
    {
        attackEnemyState = new AttackEnemyState();
        followEnemyState = new FollowEnemyState();
        idleEnemyState = new IdleEnemyState();
        _startState = idleEnemyState;
        
        OnDead += DropLoot;
    }

    private void Start()
    {
        SetState(_startState);
    }

    private void Update()
    {
        if (!_currentState.isFinished)
        {
            _currentState.Run();
        }
        else
        {
            if (searchTargets.currentTarget == null)
                SetState(idleEnemyState);
            else if (Vector3.Distance(transform.position + (Vector3)attackCenterOffset, searchTargets.currentTarget.transform.position) <= attackDistance)
                SetState(attackEnemyState);
            else 
                SetState(followEnemyState);
        }
    }

    private void SetState(EnemyState state)
    {
        _currentState = state;
        _currentState.character = this;
        _currentState.Init();
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
        DataHolder.ActiveEnemies.Remove(this);
        DataHolder.EnemyPull.PushToPull(this);
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

    protected void DropLoot()
    {
        foreach (var item in loot)
        {
            // добавить проверку на количество
            
            float randomValue = (float)Random.Range(0f,1f);
            if (randomValue <= item.DropChance)
            {
                item.CreateDropItem(this.transform.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, seeDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)attackCenterOffset, attackDistance);
        
    }
}

