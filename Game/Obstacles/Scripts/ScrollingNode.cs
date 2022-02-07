using System;
using FOAnniversary;
using Godot;
using Array = Godot.Collections.Array;

public class ScrollingNode : Node2D
{
    [Export] public int MaxChildrenCount;
    [Export] public int SpawnXPosition;
    [Export] public int Speed;
    [Export] private Resource _childResource;

    [Signal]
    public delegate void OnChildSpawn();

    [Signal]
    public delegate void OnChildSwap();

    private PackedScene _childScene;

    public override void _Ready()
    {
        var path = _childResource.ResourcePath;
        _childScene = ResourceLoader.Load<PackedScene>(path);
    }

    public void Scroll()
    {
        Position += Vector2.Left * Speed;
        SpawnXPosition += Speed;
    }

    public void ManageChildren()
    {
        var children = GetChildren();
        Node2D node = children.Count < MaxChildrenCount ? SpawnChild() : SwapChild(children);
        node.Position = new Vector2(SpawnXPosition, node.Position.y);
    }

    private Node2D SwapChild(Array children)
    {
        var node = (Node2D)children[0];
        MoveChild(node, MaxChildrenCount - 1);
        EmitSignal(nameof(OnChildSwap));
        return node;
    }

    private Node2D SpawnChild()
    {
        var node = _childScene.Instance<Node2D>();
        AddChild(node);
        EmitSignal(nameof(OnChildSpawn), node);
        return node;
    }
}