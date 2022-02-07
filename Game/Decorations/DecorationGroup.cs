using System;
using System.Collections.Generic;
using System.Linq;
using FOAnniversary.Game.Obstacles.Scripts;
using Godot;

namespace FOAnniversary.Game.Decorations
{
    public class DecorationGroup : Node2D, IScrollable
    {
        [Export()] private List<NodePath> _decorationPaths;
        private List<Sprite> _decorations;
        private Sprite _currentDecoration;

        private readonly Random _rng = new Random();

        public override void _Ready()
        {
            _decorations = _decorationPaths.Select(GetNode<Sprite>).ToList();
            Shuffle();
        }

        public void Shuffle()
        {
            if (_decorations.Count < 1)
            {
                return;
            }

            var idx = _rng.Next(0, _decorations.Count);
            if (_currentDecoration != null)
            {
                _currentDecoration.Visible = false;
            }

            _currentDecoration = _decorations[idx];
            _currentDecoration.Visible = true;
        }
    }
}