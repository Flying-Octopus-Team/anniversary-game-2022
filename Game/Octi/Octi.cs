using Godot;
using System;

public class Octi : Node2D
{
    [Signal]
    public delegate void OnDeath();


    public void EmitOnDeath()
    {
        EmitSignal(nameof(OnDeath));
    }
}
