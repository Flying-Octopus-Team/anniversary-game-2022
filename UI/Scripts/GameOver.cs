using Godot;

public class GameOver : Control
{
    [Signal]
    public delegate void RestartButtonPressed();

    [Export()] private NodePath _scoreLabelPath;
    private Label _scoreLabel;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>(_scoreLabelPath);
    }

    public void SetScore(int score)
    {
        _scoreLabel.Text = score.ToString();
    }

    public void _on_RestartButton_pressed()
    {
        EmitSignal(nameof(RestartButtonPressed));
    }
}