using Godot;

namespace FOAnniversary.Obstacles.Scripts
{
    public class Obstacle : Node2D
    {
        [Export()] public bool IsBottom;
        [Export()] private NodePath _ballSpritePath;
        [Export()] private NodePath _chainSpritePath;

        private ChainSprite _chainSprite;
        private Sprite _ballSprite;

        public override void _Ready()
        {
            _chainSprite = GetNode<ChainSprite>(_chainSpritePath);
            _ballSprite = GetNode<Sprite>(_ballSpritePath);
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
    }
}