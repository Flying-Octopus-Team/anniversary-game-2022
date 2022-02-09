using Godot;

public class ScoringManager : Node
{
    [Export()] private NodePath _labelPath;
    private Label _label;
    private int _score;

    public override void _Ready()
    {
        _label = GetNode<Label>(_labelPath);
        _score = 0;
    }

    private void OnObstaclePassed()
    {
        if (!GameManager.IsPlaying)
        {
            return;
        }

        _score++;
        _label.Text = _score.ToString();
    }
}