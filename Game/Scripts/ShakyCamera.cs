using Godot;
using System;

public class ShakyCamera : Camera2D
{
    private float _duration = 0.0f;
    private float _periodInMs = 0.0f;
    private float _amplitude = 0.0f;
    private float _timer = 0.0f;
    private float _lastShookTimer = 0f;
    private float _previousX = 0.0f;
    private float _previousY = 0.0f;
    private Vector2 _lastOffset = new Vector2(0, 0);

    private readonly Random _random = new Random();

    public override void _Ready()
    {
        SetProcess(true);
    }

    public override void _Process(float delta)
    {
        // # Only shake when there's shake time remaining.
        if (_timer == 0)
        {
            return;
        }

// # Only shake on certain frames.
        _lastShookTimer += delta;
// # Be mathematically correct in the face of lag; usually only happens once.
        while (_lastShookTimer >= _periodInMs)
        {
            _lastShookTimer -= _periodInMs;
// # Lerp between [amplitude] and 0.0 intensity based on remaining shake time.
            var intensity = _amplitude * (1 - ((_duration - _timer) / _duration));
// # Noise calculation logic from http: //jonny.morrill.me/blog/view/14
            var newX = GetRandomNumber(-1.0f, 1.0f);
            var xComponent = intensity * (_previousX + (delta * (newX - _previousX)));
            var newY = GetRandomNumber(-1.0f, 1.0f);
            var yComponent = intensity * (_previousY + (delta * (newY - _previousY)));
            _previousX = newX;
            _previousY = newY;
// # Track how much we've moved the offset, as opposed to other effects.
            var newOffset = new Vector2(xComponent, yComponent);
            Offset = (Offset - _lastOffset + newOffset);
            _lastOffset = newOffset;
        }

// # Reset the offset when we're done shaking.
        _timer -= delta;
        if (_timer <= 0)
        {
            _timer = 0;
            Offset -= _lastOffset;
        }
    }

    public void Shake(float duration, float frequency, float amplitude)
    {
        _duration = duration;
        _timer = duration;
        _periodInMs = 1.0f / frequency;
        _amplitude = amplitude;
        _previousX = GetRandomNumber(-1.0f, 1.0f);
        _previousY = GetRandomNumber(-1.0f, 1.0f);
// # Reset previous offset, if any.
        Offset -= _lastOffset;
        _lastOffset = new Vector2(0, 0);
    }

    private float GetRandomNumber(float minimum, float maximum)
    {
        return (float)_random.NextDouble() * (maximum - minimum) + minimum;
    }
}