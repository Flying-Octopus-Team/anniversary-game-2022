using Godot;
using System;

public class Octi : KinematicBody2D
{
    private int _jump_speed = -350;
    private Vector2 _velocity = new Vector2();
    private int _gravity = 10;
    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("jump"))
        {
            _velocity.y = _jump_speed;
        }
        MoveAndSlide(_velocity);
        _velocity.y += _gravity;
    }
}
