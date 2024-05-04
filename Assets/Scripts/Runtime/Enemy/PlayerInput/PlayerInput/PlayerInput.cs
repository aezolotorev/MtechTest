using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInput
{    
    public IInput input;   
    private Joystick m_joystick;
    [SerializeField] private Joystick joystickPrefab;
    [SerializeField] private Vector2 m_Sensitivity = Vector2.one;
    [SerializeField] private Slider sensSlider;
    private bool m_InvertX;
    private bool m_InvertY;
    private float invertX, invertY;
    private bool isActive=true;
    
    [SerializeField] private Transform joystickHook;

    

    
    [Inject] 
    public void Construct(IInput Iinput)
    {
        input  = Iinput;       
    }

    
    /*private void Start()
    {
        Setup();       
    }


    private void Setup()
    {
        if (Application.isMobilePlatform)
        {        
            m_joystick = Instantiate(joystickPrefab, joystickHook);              
        }        
    }*/

    public void Tick()
    {       
        Debug.Log("Tick");
        input.TickUpdate();   
    }    
}

