using Godot;
using System;

public class GameOver : Control
{

    [Signal]
    public delegate void RestartButtonPressed();
    public void _on_RestartButton_pressed()
    {
        EmitSignal(nameof(RestartButtonPressed));
    }
}
