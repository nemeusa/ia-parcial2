using System.Collections.Generic;

public class FSM<T>
{
    Dictionary<T, State> _states = new();

    State _CurrentState;

    private T _currentKey;
    public T CurrentStateKey => _currentKey;

    public void AddState(T newState, State state)
    {
        if (!_states.ContainsKey(newState)) _states.Add(newState, state);
    }

    public void Execute()
    {
        if (_CurrentState != null) _CurrentState.OnUpdate();
    }
    public void ChangeState(T newState)
    {
        if (!_states.ContainsKey(newState)) return;

        if (_CurrentState == _states[newState]) return;

        if (_CurrentState != null) _CurrentState.OnExit();

        _currentKey = newState;
        _CurrentState = _states[newState];

        _CurrentState.OnEnter();
    }
}
