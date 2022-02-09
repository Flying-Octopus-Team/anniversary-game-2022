using Godot;
using System;

public class GameManager : Node
{
    private GameNode _gameNode;

    [Export] private PackedScene _mainGameScene;

    [Export] private PackedScene _gameOverScene;

    [Export] private NodePath _musicPlayerPath;
    private AudioStreamPlayer _musicPlayer;

    public static bool IsPlaying = true;

    public override void _Ready()
    {
        _musicPlayer = GetNode<AudioStreamPlayer>(_musicPlayerPath);
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
        if (!IsPlaying) return;

        GD.Print("Game Over!");
        IsPlaying = false;
        _musicPlayer.Stop();
        Node gameOver = _gameOverScene.Instance();
        _gameNode.AddChild(gameOver);
        GD.Print(gameOver.GetPath());
        gameOver.Connect("RestartButtonPressed", this, nameof(RestartGame));
    }
}