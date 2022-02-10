using System;
using Godot;

namespace FOAnniversary
{
    public class ChainSprite : Sprite
    {

        private CollisionPolygon2D _collisionPolygon;
        [Export]
        private NodePath _collisionPolygonPath;

        public override void _Ready()
        {
            _collisionPolygon = GetNode<CollisionPolygon2D>(_collisionPolygonPath);
        }

        public void ResizeChainBottom(int height, int ballHeight)
        {
            var maxHeight = (int)GetViewportRect().Size.y;
            height = maxHeight - height;
            if (height < 0)
            {
                height = 0;
            }
            var newSize = new Vector2(RegionRect.Size.x, height);
            RegionRect = new Rect2(RegionRect.Position, newSize);
            _collisionPolygon.Scale = new Vector2(_collisionPolygon.Scale.x, height);
            Position = new Vector2(Position.x, ballHeight / 2f + (float)Math.Round(height / 2f));
        }

        public void ResizeChainTop(int height, int ballHeight)
        {
            height -= ballHeight;
            if (height < 0)
            {
                height = 0;
            }
            var newSize = new Vector2(RegionRect.Size.x, height + 1);
            RegionRect = new Rect2(RegionRect.Position, newSize);
            _collisionPolygon.Scale = new Vector2(_collisionPolygon.Scale.x, height);
            Position = new Vector2(Position.x, (float)Math.Round(height / 2f));
        }
    }
}