using Godot;
using System;

public class GameNode : Node2D
{
    [Signal]
    public delegate void OnDeath();

    public override void _Ready()
    {
        ((GameManager)GetParent()).GameReady();
    }

    public void EmitOnDeath()
    {
        EmitSignal(nameof(OnDeath));
    }

}