using System;
using System.Collections.Generic;

namespace SweetLibs
{
    public class State
    {
        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnStop;

        public State(Action OnStart, Action OnUpdate, Action OnStop)
        {
            if (OnStart != null)
                this.OnStart += OnStart;

            if (OnUpdate != null)
                this.OnUpdate += OnUpdate;

            if (OnStop != null)
                this.OnStop += OnStop;
        }
        
        public void AttachToState(Action OnStart, Action OnUpdate, Action OnStop)
        {
            if (OnStart != null)
                this.OnStart += OnStart;

            if (OnUpdate != null)
                this.OnUpdate += OnUpdate;

            if (OnStop != null)
                this.OnStop += OnStop;
        }
        
        public void DetachFromState(Action OnStart, Action OnUpdate, Action OnStop)
        {
            if (OnStart != null)
                this.OnStart -= OnStart;

            if (OnUpdate != null)
                this.OnUpdate -= OnUpdate;

            if (OnStop != null)
                this.OnStop -= OnStop;
        }

        internal void Start()
        {
            OnStart?.Invoke();
        }
            
        internal void Update()
        {
            OnUpdate?.Invoke();
        }
            
        internal void Stop()
        {
            OnStop?.Invoke();
        }
    }
    
    public class StateMachine<TLabel>
    {
        private readonly Dictionary<TLabel, State> stateDictionary;
        private State currentState;
        
        public TLabel CurrentState { get; private set; }
        
        public State this[TLabel key] => stateDictionary[key]; 

        public StateMachine()
        {
            stateDictionary = new Dictionary<TLabel, State>();
        }
        
        public void AddState(TLabel label, Action OnStart = null, Action OnUpdate = null, Action OnStop = null)
        {
            stateDictionary[label] = new State(OnStart, OnUpdate, OnStop);
        }
        
        public void ChangeState(TLabel newState)
        {
            currentState?.Stop();
            
            currentState = stateDictionary[newState];
            CurrentState = newState;

            currentState?.Start();
        }
        
        public void Update()
        {
            currentState.Update();
        }
    }
}
