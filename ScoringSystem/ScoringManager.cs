using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Object = Godot.Object;


public class ScoringManager : Node
{
    [Export()] private Octi _player;
    [Export()] private Octi _player2;
    private Label _label;
    private int Score = 0;
    
    
    public override void _Ready()
    {
        _label = GetChild(0).GetChild(0) as Label;
        if (_label != null)
        {
            StartCounting();
        }
    }
    
    private async void StartCounting()
    {
        while (GameManager.IsPlaying)
        {
            await ToSignal(_player, "score");
            Score++;
            _label.Text = Score.ToString();
        }
    }
        
    internal class Octi : Object
    {
    }
}