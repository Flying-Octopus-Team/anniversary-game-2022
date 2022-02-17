using System.Collections.Generic;
using System.Linq;
using Godot;

namespace FOAnniversary.Obstacles.Scripts
{
    public class Obstacle : Node2D
    {
        [Export()] public bool IsBottom;
        [Export()] private NodePath _ballSpritePath;
        [Export()] private NodePath _chainSpritePath;
        [Export()] private List<NodePath> _collisionNodesPaths;

        private ChainSprite _chainSprite;
        private Sprite _ballSprite;
        private List<CollisionPolygon2D> _collisionObjects;

        public override void _Ready()
        {
            _chainSprite = GetNode<ChainSprite>(_chainSpritePath);
            _ballSprite = GetNode<Sprite>(_ballSpritePath);
            _collisionObjects = _collisionNodesPaths.Select(GetNode<CollisionPolygon2D>).ToList();
        }

        public int GetBallHeight()
        {
            return _ballSprite.Texture.GetHeight();
        }

        public void ResizeObstacle(int obstacleHeight)
        {
            if (IsBottom)
            {
                _chainSprite.ResizeChainBottom(obstacleHeight, _ballSprite.Texture.GetHeight());
                Position = new Vector2(Position.x, obstacleHeight);
            }
            else
            {
                _chainSprite.ResizeChainTop(obstacleHeight, _ballSprite.Texture.GetHeight());
                _ballSprite.Position = new Vector2(_ballSprite.Position.x, obstacleHeight - _ballSprite.Texture.GetHeight() / 2f);
            }
        }

        public void DisableCollisions()
        {
            _collisionObjects.ForEach(obj => obj.Disabled = true);
        }

        public void EnableCollisions()
        {
            _collisionObjects.ForEach(obj => obj.Disabled = false);
        }
    }
}