using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    [SerializeField] private SearchTargets searchTargets;
    private bool cachedRotation;
    
    void Update()
    {
        bool isRight = cachedRotation;
        
        if (GunManager.ShootModeEnabled 
            && searchTargets.currentTarget != null)
        {
            isRight = IsRightDirection(searchTargets.GetDifferenceToTarget().normalized.x);
            cachedRotation = isRight;
        }
        else if(InputHandler.MoveVector.x != 0)
        {
            isRight = IsRightDirection(InputHandler.MoveVector.normalized.x);
            cachedRotation = isRight;
        }
        else
        {
            isRight = cachedRotation;
        }
        cachedRotation = isRight;
        Flip(isRight);
        
    }

    public bool IsRightDirection(float xValue)
    {
        if (xValue >= 0)
            return true;
        else
            return false;
    }
    
    private void Flip(bool isRight)
    {
        if (isRight)
        {
            Quaternion rot = transform.rotation;
            rot.y = 0;
            transform.rotation = rot;
        }
        else
        {
            Quaternion rot = transform.rotation;
            rot.y = 180;
            transform.rotation = rot;
        }
    }
}
