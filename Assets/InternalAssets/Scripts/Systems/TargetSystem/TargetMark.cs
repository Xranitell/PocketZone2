using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SearchTargets))]
public class TargetMark : MonoBehaviour
{
    private SearchTargets _searchTargets;
    private GameObject _cachedCurrentTarget;
    
    private void Awake()
    {
        _searchTargets = GetComponent<SearchTargets>();
    }

    private void Update()
    {
        if (_cachedCurrentTarget != _searchTargets.currentTarget)  //проверка для исключения повторного вызова)
        {
            _cachedCurrentTarget?.GetComponent<ITargetable>().UnTarget();
            _searchTargets.currentTarget?.GetComponent<ITargetable>().ChooseAsTarget();
            _cachedCurrentTarget = _searchTargets.currentTarget;
        }
    }
}
