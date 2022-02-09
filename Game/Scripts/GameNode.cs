using Godot;

public class GameNode : Node2D
{
    [Signal]
    public delegate void OnDeath();

    [Export()] private NodePath _scoringSystemPath;
    private ScoringManager _scoringSystem;

    public override void _Ready()
    {
        var parent = GetParent();
        if (parent != null)
        {
            ((GameManager)parent).GameReady(this);
        }

        _scoringSystem = GetNode<ScoringManager>(_scoringSystemPath);
    }

    public void EmitOnDeath()
    {
        EmitSignal(nameof(OnDeath), _scoringSystem.Score);
    }
}