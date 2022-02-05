using Godot;
using System;

public class Exit_button : Button
{
    public override void _Ready()
    {
        Connect("pressed", this, "_on_Exit_button_pressed");
    }

    public void _on_Exit_button_pressed()
    {
        GetTree().Quit();
    }
}
