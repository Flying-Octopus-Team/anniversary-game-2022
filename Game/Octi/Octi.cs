using Godot;
using System;

public class Octi : Node2D
{
    [Signal]
    public delegate void OnDeath();

    [Signal]
    public delegate void OnPassObstacles();

    [Export()] private NodePath _octiBodyPath;
    [Export()] private int _jumpSpeed;
    [Export()] private int _gravity;

    [Export()] private NodePath _audioPlayerPath;
    [Export()] private AudioStreamSample _jumpSound;
    [Export()] private AudioStreamSample _deathSound;
    [Export()] private AudioStreamSample _obstaclePassedSound;

    private Vector2 _velocity;
    private KinematicBody2D _octiBody;
    private AudioStreamPlayer _audioPlayer;

    public override void _Ready()
    {
        _velocity = new Vector2();
        _octiBody = GetNode<KinematicBody2D>(_octiBodyPath);
        _audioPlayer = GetNode<AudioStreamPlayer>(_audioPlayerPath);
    }

    private void OnBodyEntered(Node body)
    {
        if (body is CollisionObject2D obj && obj.CollisionLayer == 1)
        {
            _velocity = Vector2.Zero;
            _audioPlayer.Stream = _deathSound;
            _audioPlayer.Play();
            EmitSignal(nameof(OnDeath));
        }
    }

    public void OnBodyExit(Node body)
    {
        if (body is CollisionObject2D obj && obj.CollisionLayer == 2)
        {
            _audioPlayer.Stream = _obstaclePassedSound;
            _audioPlayer.Play();
            EmitSignal(nameof(OnPassObstacles));
        }
    }
    
    public override void _PhysicsProcess(float delta)
    {
        if (GameManager.IsPlaying)
        {
            if (Input.IsActionJustPressed("jump"))
            {
                _velocity.y = _jumpSpeed;
                _audioPlayer.Stream = _jumpSound;
                _audioPlayer.Play();
            }
        }

        _octiBody.MoveAndSlide(_velocity);
        _velocity.y += _gravity;
    }
}