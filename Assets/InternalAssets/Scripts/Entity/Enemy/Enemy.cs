
using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Entity, ITargetable, IWatcher
{
    [BoxGroup("Components")] [SerializeField] private GameObject targetMark;
    [BoxGroup("Components")] public SearchTargets searchTargets;
    [BoxGroup("Components")] public Animator animator;

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
    
    private AttackEnemyState _attackEnemyState;
    private FollowEnemyState _followEnemyState;
    private IdleEnemyState _idleEnemyState;
    
    [Expandable]public List<Item> loot = new List<Item>();
    public float SeeDistance => seeDistance;

    protected void Awake()
    {
        _attackEnemyState = new AttackEnemyState();
        _followEnemyState = new FollowEnemyState();
        _idleEnemyState = new IdleEnemyState();
        _startState = _idleEnemyState;
        
        OnDead += DropLoot;
    }

    private void Start()
    {
        SetState(_startState);
    }

    private void Update()
    {
        if (!_currentState.IsFinished)
        {
            _currentState.Run();
        }
        else
        {
            if (searchTargets.currentTarget == null)
                SetState(_idleEnemyState);
            else if (Vector3.Distance(transform.position + (Vector3)attackCenterOffset, searchTargets.currentTarget.transform.position) <= attackDistance)
                SetState(_attackEnemyState);
            else 
                SetState(_followEnemyState);
        }
    }

    private void SetState(EnemyState state)
    {
        _currentState = state;
        _currentState.character = this;
        _currentState.Init();
    }

    public void Attack()
    {
        searchTargets.currentTarget?.GetComponent<IDamaged>().GetDamage(damageAttack);
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

    private void DropLoot()
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

