using Godot;
using System;

public class GameOver : Node
{

    [Signal]
    public delegate void RestartButtonPressed();
    public void _on_RestartButton_pressed()
    {
        EmitSignal(nameof(RestartButtonPressed));
    }
}
