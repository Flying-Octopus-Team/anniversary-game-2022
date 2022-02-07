using System;
using Godot;

namespace FOAnniversary
{
    public class ChainSprite : Sprite
    {
        public void ResizeChainBottom(int height, int ballHeight)
        {
            var maxHeight = (int)GetViewportRect().Size.y;
            height = maxHeight - height;
            if (height < 0 )
            {
                height = 0;
            }
            var newSize = new Vector2(RegionRect.Size.x, height);
            RegionRect = new Rect2(RegionRect.Position, newSize);
            Position = new Vector2(Position.x, ballHeight / 2f + (float)Math.Round(height / 2f));
        }
        
        public void ResizeChainTop(int height, int ballHeight)
        {
            height -= ballHeight;
            if (height < 0 )
            {
                height = 0;
            }
            var newSize = new Vector2(RegionRect.Size.x, height + 1);
            RegionRect = new Rect2(RegionRect.Position, newSize);
            Position = new Vector2(Position.x, (float)Math.Round(height / 2f));
        }
    }
}