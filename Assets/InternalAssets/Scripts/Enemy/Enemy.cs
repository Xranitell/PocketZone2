using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, ITargetable
{
    [SerializeField] private GameObject targetMark;
    
    
    
    
    public void ChooseAsTarget()
    {
        targetMark.SetActive(true);
    }

    public void UnTarget()
    {
        targetMark.SetActive(false);
    }
}
