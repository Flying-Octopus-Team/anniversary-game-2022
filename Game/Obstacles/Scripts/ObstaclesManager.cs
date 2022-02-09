using System;
using FOAnniversary.Game.Obstacles.Scripts;
using Godot;

namespace FOAnniversary.Obstacles.Scripts
{
    public class ObstaclesManager : Node2D
    {
        private readonly Random _random = new Random();

        [Export] private float _interval;
        [Export] private float _intervalVariance;
        [Export] private float _intervalIncreaseValue;

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
                await ToSignal(_obstaclesTimer, "timeout");

                var variance = (int)(_intervalVariance * 100);
                var varianceValue = _random.Next(-variance, variance) / 100f;
                _obstaclesTimer.WaitTime = _interval + varianceValue;

                _obstaclesScroll.ManageChildren();
                if (_interval > 0.1f)
                {
                    _interval += _intervalIncreaseValue;
                    if (_interval < 0.1f)
                    {
                        _interval = 0.1f;
                    }
                }
                if (_intervalVariance > 0)
                {
                    _intervalVariance += _intervalIncreaseValue * 0.1f;
                    if (_intervalVariance < 0.05f)
                    {
                        _intervalVariance = 0.05f;
                    }
                }
                
            }
        }
    }
}