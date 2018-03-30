using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace newvisionsproject.states.ball
{
  public interface IBallState
  {
    IBallState SetAsNextState();
    void Tick();
    void OnEnter();
    void OnUpdate();
    IBallState OnExitTo(BallStates nextState);
  }

	/**
  * This is an enumeration over the states
  * the ball can adopt */
  public enum BallStates{
    Moving,
    outOfBounds
  }
}