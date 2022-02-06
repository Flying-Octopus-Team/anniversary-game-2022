using Godot;
using System;

public class GameManager : Node
{

    [Export]
    private NodePath _mainGame;

    [Export]
    private PackedScene _mainGameScene;

    [Export]
    private PackedScene _gameOverScene;

    public static bool IsPlaying = true;

    public void GameReady(Node node)
    {
        node.Connect("OnDeath", this, nameof(EndGame));
    }
    public void RestartGame()
    {
        GetNode(_mainGame).Disconnect("OnDeath", this, nameof(EndGame));
        GetNode(_mainGame).QueueFree();
        IsPlaying = true;
        Node mainGame = _mainGameScene.Instance();
        AddChild(mainGame);
        _mainGame = mainGame.GetPath();
    }

    public void EndGame()
    {
        if (!IsPlaying) return;

        GD.Print("Game Over!");
        IsPlaying = false;
        Node gameOver = _gameOverScene.Instance();
        GetNode(_mainGame).AddChild(gameOver);
        GD.Print(gameOver.GetPath());
        GetNode(gameOver.GetPath()).Connect("RestartButtonPressed", this, nameof(RestartGame));
    }
}
