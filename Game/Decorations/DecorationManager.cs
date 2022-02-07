using Godot;
using System;
using FOAnniversary.Game.Obstacles.Scripts;

public class DecorationManager : Node2D
{
    private readonly Random _random = new Random();

    [Export] private float _interval;
    [Export] private float _intervalVariance;

    [Export] private NodePath _obstaclesScrollPath;
    [Export] private NodePath _timerPath;

    private ScrollingNode _obstaclesScroll;
    private Timer _obstaclesTimer;

    public override void _Ready()
    {
        _obstaclesTimer = GetNode<Timer>(_timerPath);
        _obstaclesScroll = GetNode<ScrollingNode>(_obstaclesScrollPath);
        StartGenerating();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (GameManager.IsPlaying)
        {
            _obstaclesScroll.Scroll();                
        }
    }

    public void _on_Obstacles_OnChildSwap(Node2D node)
    {
        ((IScrollable)node).Shuffle();
    }

    private async void StartGenerating()
    {
        while (GameManager.IsPlaying)
        {
            var tempVar = (int)(_intervalVariance * 100);
            var finalVar = _random.Next(-tempVar, tempVar) / 100f;
            _obstaclesTimer.WaitTime = _interval + finalVar;
            
            await ToSignal(_obstaclesTimer, "timeout");
            
            _obstaclesScroll.ManageChildren();
        }
    }
}
