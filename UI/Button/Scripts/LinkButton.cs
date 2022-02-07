using Godot;
using System;

public class LinkButton : Node
{
    [Export()] private String _url;

    public override void _Ready()
    {
        Connect("pressed", this, "OnButtonPressed");
    }

    public void OnButtonPressed()
    {
        OS.ShellOpen(_url);
    }
}
