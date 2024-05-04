using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput :  IInput
{
    public Vector2 Move
    {
        get { return move; }
        set { move = value; }
    }
    public Vector2 move;   
    public bool attack { get; set; }      
    private Joystick m_Joystick;
    [SerializeField] private int m_TouchLimit = 10;
    private EventSystem m_EventStytem;
    private List<string> m_AvailableTouchesId = new List<string>();
    private Func<Touch, bool> m_IsTouchAvailable;
    private Vector2 Input_Movement
    {
        get { return new Vector2(m_Joystick.Horizontal, m_Joystick.Vertical); }
    }

    public MobileInput(Joystick joystick)
    {
        m_Joystick = joystick;
        if (EventSystem.current != null)
            m_EventStytem = EventSystem.current;   
    }

    public void TickUpdate()
    {      
        move.x = Input_Movement.x;
        move.y = Input_Movement.y;
    }
}
