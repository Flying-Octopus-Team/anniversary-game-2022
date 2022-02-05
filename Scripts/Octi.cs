using Godot;
using System;
using FOAnniversary.Obstacles.Scripts;

public class Octi : KinematicBody2D
{
    [Export]
    private int _JumpSpeed;
    [Export]
    private int _Gravity;
    
    private Vector2 _Velocity = new Vector2();

    public override void _PhysicsProcess(float delta)
    {
        if (GameManager.IsPlaying)
        {
            if (Input.IsActionJustPressed("jump"))
            {
                _Velocity.y = _JumpSpeed;
            }
        }
        MoveAndSlide(_Velocity);
        _Velocity.y += _Gravity;
    }
    
    private void OnBodyEntered(Node body)
    {
        CollisionObject2D obj = body as CollisionObject2D;
        if (obj.CollisionLayer != 1)
        {
            return;
        }
        if (GameManager.IsPlaying){}
            _Velocity = Vector2.Zero;
            GameManager.EndGame();
    }
}
