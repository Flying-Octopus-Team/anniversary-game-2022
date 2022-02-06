using Godot;
using System;

public class ExitButton : TextureButton
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
