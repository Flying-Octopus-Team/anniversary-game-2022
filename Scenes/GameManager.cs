using Godot;
using System;

    public class GameManager : Node
    {
        public static bool IsPlaying = true;

        public static void RestartGame()
        {
            
        }

        public static void EndGame()
        {
            if (!IsPlaying) return;

            GD.Print("Game Over!");
            IsPlaying = false;
        }
    }
