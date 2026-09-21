namespace ChronoTactics.Engine;

using System;

public class GameLoop
{
    public bool IsRunning { get; private set; }
    private Action _onUpdate;
    private Action _onRender;

    public GameLoop(Action onUpdate, Action onRender)
    {
        _onUpdate = onUpdate;
        _onRender = onRender;
        IsRunning = false;
    }

    public void Start()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public void Tick()
    {
        if (!IsRunning) return;
        _onUpdate?.Invoke();
        _onRender?.Invoke();
    }
}