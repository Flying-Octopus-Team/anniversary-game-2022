using Godot;
using System;

public class GameManager : Node
{
    private GameNode _gameNode;

    [Export] private PackedScene _mainGameScene;

    [Export] private PackedScene _gameOverScene;

    public static bool IsPlaying = true;

    public override void _Ready()
    {
        StartGame();
    }

    public void StartGame()
    {
        IsPlaying = true;
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
        Node gameOver = _gameOverScene.Instance();
        _gameNode.AddChild(gameOver);
        GD.Print(gameOver.GetPath());
        gameOver.Connect("RestartButtonPressed", this, nameof(RestartGame));
    }
}