using Godot;
using System;

public class SwitchSceneWithStringButton : TextureButton
{
    [Export] private String _sceneResourcePath;

    public override void _Ready()
    {
        Connect("pressed", this, "OnStartGameButtonPressed");
    }

    public void OnStartGameButtonPressed()
    {
        GetTree().ChangeScene(_sceneResourcePath);
    }
}