using Godot;

public class GameManager : Node
{
    private GameNode _gameNode;

    [Export] private PackedScene _mainGameScene;

    [Export] private NodePath _gameOverPath;
    private Control _gameOver;

    [Export] private NodePath _gamePositionPath;
    private Node _gamePosition;

    [Export] private NodePath _musicPlayerPath;
    private AudioStreamPlayer _musicPlayer;

    [Export] private NodePath _shakyCameraPath;
    private ShakyCamera _shakyCamera;

    public static bool IsPlaying = true;

    public override void _Ready()
    {
        _musicPlayer = GetNode<AudioStreamPlayer>(_musicPlayerPath);
        _shakyCamera = GetNode<ShakyCamera>(_shakyCameraPath);
        _gameOver = GetNode<Control>(_gameOverPath);
        _gamePosition = GetNode<Node>(_gamePositionPath);
        StartGame();
    }

    public void StartGame()
    {
        IsPlaying = true;
        _musicPlayer.Play();
        GameNode mainGame = _mainGameScene.Instance<GameNode>();
        AddChildBelowNode(_gamePosition, mainGame);
    }

    public void GameReady(GameNode gameNode)
    {
        gameNode.Connect("OnDeath", this, nameof(EndGame));
        _gameNode = gameNode;
    }

    public void RestartGame()
    {
        _gameNode.Disconnect("OnDeath", this, nameof(EndGame));
        _gameNode.QueueFree();
        _gameNode = null;
        _gameOver.Visible = false;
        StartGame();
    }

    public void EndGame()
    {
        _shakyCamera.Shake(0.2f, 15f, 8f);
        if (!IsPlaying)
        {
            return;
        }

        IsPlaying = false;
        _musicPlayer.Stop();
        _gameOver.Visible = true;
    }
}