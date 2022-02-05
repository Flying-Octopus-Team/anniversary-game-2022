using Godot;
using System;

public class ExitButton : Button
{
    public override void _Ready()
    {
        Connect("pressed", this, "OnExitButtonPressed");
    }

    public void OnExitButtonPressed()
    {
        GetTree().Quit();
    }
}
