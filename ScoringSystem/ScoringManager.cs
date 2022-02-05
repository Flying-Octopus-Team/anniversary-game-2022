using System;
using System.Collections.Generic;
using System.Linq;
using FOAnniversary.Obstacles.Scripts;
using Godot;
using Object = Godot.Object;

public class ScoringManager : Node
{
    [Export()] private Octi _player;
    private Label _label;
    private int Score = 0;
    public override void _Ready()
    {
        _label = GetChild(0).GetChild(0) as Label;
    }

    private void OnBodyEntered(Node body)
    {
        if (!GameManager.IsPlaying)
        {
            return;
        }
        CollisionObject2D obj = body as CollisionObject2D;
        if (obj.CollisionLayer == 2)
        {
            // Update Score
            Score++;
            _label.Text = Score.ToString();
        }
    }
}