using Godot;
using System;

public class BackgroundColor : Sprite
{
    private Vector2 _size;
    public override void _Ready()
    {
        _size = this.RegionRect.Size;
        Vector2 newSize = GetViewportRect().Size;
        ChangeSize(newSize);
    }

    public override void _Process(float delta)
    {
        Vector2 newSize = GetViewportRect().Size;
        ChangeSize(newSize);
    }

    private void ChangeSize(Vector2 newSize)
    {
        if (newSize != _size)
        {
            _size = newSize;
            this.RegionRect = new Rect2(0, 0, newSize.x + 10, newSize.y + 10);
        }
    }

}
