using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    public static Vector3 MoveVector { get; private set; }

    private void Update()
    {
        //mobile device
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            MoveVector = joystick.Direction;
        }
        else
        {
            MoveVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        }
    }

}
