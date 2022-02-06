using Godot;
using System;

public class StartGameButton : TextureButton
{

    [Export]
    private PackedScene _MainScene;

    public override void _Ready()
    {
        Connect("pressed", this, "OnStartGameButtonPressed");
    }

    public void OnStartGameButtonPressed()
    {
        GetTree().ChangeScene(_MainScene.ResourcePath);
    }
}
