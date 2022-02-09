using Godot;

public class GameManager : Node
{
    private GameNode _gameNode;

    [Export] private PackedScene _mainGameScene;

    [Export] private PackedScene _gameOverScene;

    [Export] private NodePath _musicPlayerPath;
    private AudioStreamPlayer _musicPlayer;

    [Export] private NodePath _shakyCameraPath;
    private ShakyCamera _shakyCamera;

    public static bool IsPlaying = true;

    public override void _Ready()
    {
        _musicPlayer = GetNode<AudioStreamPlayer>(_musicPlayerPath);
        _shakyCamera = GetNode<ShakyCamera>(_shakyCameraPath);
        StartGame();
    }

    public void StartGame()
    {
        IsPlaying = true;
        _musicPlayer.Play();
        GameNode mainGame = _mainGameScene.Instance<GameNode>();
        AddChild(mainGame);
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
        Node gameOver = _gameOverScene.Instance();
        _gameNode.AddChild(gameOver);
        GD.Print(gameOver.GetPath());
        gameOver.Connect("RestartButtonPressed", this, nameof(RestartGame));
    }
}