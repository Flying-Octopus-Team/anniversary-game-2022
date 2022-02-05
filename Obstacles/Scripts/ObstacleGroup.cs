using System;
using System.Collections.Generic;
using System.Linq;
using FOAnniversary.Obstacles.Scripts;
using Godot;

namespace FOAnniversary
{
    public class ObstacleGroup : Node2D
    {
        [Export()] private float _speed;
        [Export()] private List<NodePath> _topObstaclePaths;
        [Export()] private List<NodePath> _bottomObstaclePaths;

        private List<Obstacle> _topObstacles;
        private List<Obstacle> _bottomObstacles;
        private Obstacle _currentTopObstacle;
        private Obstacle _currentBottomObstacle;

        private Random rng = new Random();

        public override void _Ready()
        {
            _topObstacles = _topObstaclePaths.Select(GetNode<Obstacle>).ToList();
            _bottomObstacles = _bottomObstaclePaths.Select(GetNode<Obstacle>).ToList();
            Shuffle();
        }

        public void _on_TextureButton_pressed()
        {
            Shuffle();
        }

        public void Shuffle()
        {
            var maxHeight = (int)GetViewportRect().Size.y;
            
            var topHeight = rng.Next(0, maxHeight - 80 - 80);
            var bottomHeight = rng.Next(topHeight + 80, maxHeight);

            ShuffleBottomObstacle(bottomHeight);
            ShuffleTopObstacle(topHeight);
        }

        private void ShuffleBottomObstacle(float bottomHeight)
        {
            if (_topObstacles.Count < 1)
            {
                return;
            }

            var botIdx = (int)rng.Next(0, _bottomObstacles.Count);
            if (_currentBottomObstacle != null)
            {
                _currentBottomObstacle.Visible = false;
            }

            _currentBottomObstacle = _bottomObstacles[botIdx];
            _currentBottomObstacle.Visible = true;
            _currentBottomObstacle.ResizeObstacle((int)bottomHeight);
        }

        private void ShuffleTopObstacle(float topHeight)
        {
            if (_topObstacles.Count < 1)
            {
                return;
            }

            var botIdx = rng.Next(0, _topObstacles.Count);
            if (_currentTopObstacle != null)
            {
                _currentTopObstacle.Visible = false;
            }

            _currentTopObstacle = _topObstacles[botIdx];
            _currentTopObstacle.Visible = true;
            _currentTopObstacle.ResizeObstacle((int)topHeight);
        }


        public override void _PhysicsProcess(float delta)
        {
            Position += Vector2.Left * _speed;
        }
    }
}