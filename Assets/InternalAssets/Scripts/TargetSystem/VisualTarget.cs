using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SearchTargets))]
public class VisualTarget : MonoBehaviour
{
    private SearchTargets _searchTargets;
    private GameObject cachedCurrentTarget;
    
    private void Awake()
    {
        _searchTargets = GetComponent<SearchTargets>();
    }

    private void Update()
    {
        if (cachedCurrentTarget != _searchTargets.currentTarget  //проверка для исключения повторного вызова
            && _searchTargets.currentTarget)
        {
            cachedCurrentTarget?.GetComponent<ITargetable>().UnTarget();
            _searchTargets.currentTarget.GetComponent<ITargetable>().ChooseAsTarget();
            cachedCurrentTarget = _searchTargets.currentTarget;
        }
    }
}
