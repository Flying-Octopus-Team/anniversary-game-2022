using Godot;

public class ScoringManager : Node
{
    [Export()] private NodePath _labelPath;
    private Label _label;
    public int Score;

    public override void _Ready()
    {
        _label = GetNode<Label>(_labelPath);
        Score = 0;
    }

    private void OnObstaclePassed()
    {
        if (!GameManager.IsPlaying)
        {
            return;
        }

        Score++;
        _label.Text = Score.ToString();
    }
}