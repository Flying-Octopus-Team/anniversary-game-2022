using Godot;
using System;

public class Octi : KinematicBody2D
{
    [Export]
    private int _JumpSpeed;
    [Export]
    private int _Gravity;

    private Vector2 _Velocity = new Vector2();
    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            _Velocity.y = _JumpSpeed;
        }
        MoveAndSlide(_Velocity);
        _Velocity.y += _Gravity;
    }
}
