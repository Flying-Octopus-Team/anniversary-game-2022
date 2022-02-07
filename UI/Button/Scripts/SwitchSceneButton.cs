using Godot;
using System;

public class SwitchSceneButton : TextureButton
{

    [Export]
    private PackedScene _scene;

    public override void _Ready()
    {
        Connect("pressed", this, "OnStartGameButtonPressed");
    }

    public void OnStartGameButtonPressed()
    {
        GetTree().ChangeScene(_scene.ResourcePath);
    }
}
