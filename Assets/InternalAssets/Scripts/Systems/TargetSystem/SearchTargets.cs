using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IWatcher))]
public class SearchTargets : MonoBehaviour
{
    public string targetTag = "Enemy";

    public GameObject currentTarget;
    private IWatcher _watcher;
    private void Awake()
    {
        _watcher = GetComponent<IWatcher>();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _watcher.SeeDistance);
        GameObject[] targets = FilterTargetsByTag(colliders);
        currentTarget = GetClosestTarget(targets);
    }

    private GameObject[] FilterTargetsByTag(Collider2D[] colliders)
    {
        List<GameObject> targets = new List<GameObject>();
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                targets.Add(collider.gameObject);
            }
        }
        return targets.ToArray();
    }

    private GameObject GetClosestTarget(GameObject[] targets)
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach (GameObject target in targets)
        {
            float distanceToTarget = Vector3.Distance(currentPosition, target.transform.position);
            
            if (distanceToTarget < closestDistance)
            {
                closestTarget = target;
                closestDistance = distanceToTarget;
            }
        }
        return closestTarget;
    }
    
    private void OnDrawGizmosSelected()
    {
        if(_watcher is null) return;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _watcher.SeeDistance);
    }

    public Vector3 GetDifferenceToTarget()
    {
        return (currentTarget.transform.position - transform.position);
    }
}
