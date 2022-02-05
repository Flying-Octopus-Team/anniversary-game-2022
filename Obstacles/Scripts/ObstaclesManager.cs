using System;
using FOAnniversary;
using Godot;
using Array = Godot.Collections.Array;


public class ObstaclesManager : Node2D
    {
        [Export()] private Resource _obstacle;
        [Export()] private int _spawnAtX; 
    
        [Export()] private float _interval;
        [Export()] private float _intervalVariance;
        [Export()] private int _speed;

        private PackedScene _obstaclePrefab;
        private const int MAX_OBSTACLES = 10;
    
        private Random _random = new Random();

        private Node2D _obstaclesParent; 
        private Timer _obstaclesTimer;
    
        public override void _Ready()
        {
        var i = 5;
            _obstaclesTimer = GetChild<Timer>(0);
            _obstaclesParent = (Node2D)GetChild(2);
        
            if (_obstacle == null)
            {
                GD.PrintErr("No prefab attached to manager.");
                return;
            }
            string path = _obstacle.ResourcePath;
            _obstaclePrefab = ResourceLoader.Load<PackedScene>(path);
        
            StartGenerating();
        }
    
        public override void _PhysicsProcess(float delta)
        {
            // Moves obstacles
            _obstaclesParent.Position += Vector2.Left * _speed;
            _spawnAtX += _speed;
        }

        private async void StartGenerating()
        {
            while (GameManager.IsPlaying)
            {
                await ToSignal(_obstaclesTimer, "timeout");

                int tempVar = (int)(_intervalVariance * 100);
                float finalVar = _random.Next(-tempVar, tempVar) / 100f;
                _obstaclesTimer.WaitTime = _interval + finalVar;         
            
                ManageObstacles(_spawnAtX);
            }
        }

        private void ManageObstacles(int spawnX)
        {
            Node2D node;
            Array children = _obstaclesParent.GetChildren();
            if (children.Count < MAX_OBSTACLES)
            {
                // Spawn
                node = _obstaclePrefab.Instance<ObstacleGroup>();
                _obstaclesParent.AddChild(node);
            }
            else
            {
                node = (ObstacleGroup) children[0];
                _obstaclesParent.MoveChild(node, MAX_OBSTACLES - 1);
            }

            // Reposition and attach
            Vector2 newPos = new Vector2(spawnX, 0);
            node.Position = newPos;
        }
    }

    public static class GameManager
    {
        public static bool IsPlaying = true;
    }
