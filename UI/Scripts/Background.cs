using Godot;
using System;

public class Background : Sprite
{
    private float _width;
    public override void _Ready()
    {
        _width = this.RegionRect.Size.x;
        float newWidth = GetViewportRect().Size.x;
        ChangeWidth(newWidth);
    }

    public override void _Process(float delta)
    {
        float newWidth = GetViewportRect().Size.x;
        ChangeWidth(newWidth);
    }

    private void ChangeWidth(float newWidth)
    {
        if (newWidth != _width)
        {
            _width = newWidth;
            this.RegionRect = new Rect2(0, 0, _width + 10, this.RegionRect.Size.y);
        }
    }
}
