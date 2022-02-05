using Godot;
using System;

public class Start_game_button : Button
{
    public override void _Ready()
    {
        Connect("pressed", this, "_on_Start_Game_button_pressed");
    }

    public void _on_Start_Game_button_pressed()
    {
        GetTree().ChangeScene("res://Scenes/Main_game.tscn");
    }
}
