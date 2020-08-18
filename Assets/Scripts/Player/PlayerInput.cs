using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions input;
    float _dir;
    public float dir {get {return _dir;}}
    bool _jumpTrig;
    public bool jumpTrig {get {return _jumpTrig;} set  {_jumpTrig = value;} }
    void Start()
    {
        input = new PlayerInputActions();
        input.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _dir = input.Player.Move.ReadValue<float>();
        if(input.Player.Jump.ReadValue<float>()>0)
        {
            _jumpTrig = true;
        } else _jumpTrig = false;
    }
}
