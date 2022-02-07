using System;
using Godot;

namespace FOAnniversary.Obstacles.Scripts
{
    public class ObstaclesManager : Node2D
    {
        private const int MAX_OBSTACLES = 10;
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
            ((ObstacleGroup)node).Shuffle();
        }

        private async void StartGenerating()
        {
            while (GameManager.IsPlaying)
            {
                await ToSignal(_obstaclesTimer, "timeout");

                var tempVar = (int)(_intervalVariance * 100);
                var finalVar = _random.Next(-tempVar, tempVar) / 100f;
                _obstaclesTimer.WaitTime = _interval + finalVar;

                _obstaclesScroll.ManageChildren();
            }
        }
    }
}