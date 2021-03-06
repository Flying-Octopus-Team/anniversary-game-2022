using System;
using System.Collections.Generic;
using System.Linq;
using FOAnniversary.Game.Obstacles.Scripts;
using FOAnniversary.Obstacles.Scripts;
using Godot;

namespace FOAnniversary
{
    public class ObstacleGroup : Node2D, IScrollable
    {
        [Export()] private List<NodePath> _topObstaclePaths;
        [Export()] private List<NodePath> _bottomObstaclePaths;
        [Export()] private int _gapSize;

        private List<Obstacle> _topObstacles;
        private List<Obstacle> _bottomObstacles;
        private Obstacle _currentTopObstacle;
        private Obstacle _currentBottomObstacle;

        private readonly Random _rng = new Random();

        public override void _Ready()
        {
            _topObstacles = _topObstaclePaths.Select(GetNode<Obstacle>).ToList();
            _bottomObstacles = _bottomObstaclePaths.Select(GetNode<Obstacle>).ToList();
            Shuffle();
        }

        public void Shuffle()
        {
            ShuffleBottomObstacle();
            ShuffleTopObstacle();
            var maxHeight = (int)GetViewportRect().Size.y;
            var topHeight = _rng.Next(0, maxHeight - _gapSize);
            var bottomHeight = _rng.Next(topHeight + _gapSize, maxHeight);
            _currentBottomObstacle.ResizeObstacle(bottomHeight);
            _currentTopObstacle.ResizeObstacle(topHeight);
        }

        private void ShuffleBottomObstacle()
        {
            if (_topObstacles.Count < 1)
            {
                return;
            }

            _bottomObstacles.ForEach(obs => obs.DisableCollisions());
            var botIdx = _rng.Next(0, _bottomObstacles.Count);
            if (_currentBottomObstacle != null)
            {
                _currentBottomObstacle.Visible = false;
            }

            _currentBottomObstacle = _bottomObstacles[botIdx];
            _currentBottomObstacle.Visible = true;
            _currentBottomObstacle.EnableCollisions();;
        }

        private void ShuffleTopObstacle()
        {
            if (_topObstacles.Count < 1)
            {
                return;
            }

            _topObstacles.ForEach(obs => obs.DisableCollisions());
            var idx = _rng.Next(0, _topObstacles.Count);
            if (_currentTopObstacle != null)
            {
                _currentTopObstacle.Visible = false;
            }

            _currentTopObstacle = _topObstacles[idx];
            _currentTopObstacle.Visible = true;
            _currentTopObstacle.EnableCollisions();;
        }
    }
}