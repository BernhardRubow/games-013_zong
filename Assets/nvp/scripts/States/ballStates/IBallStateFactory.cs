using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace newvisionsproject.states.ball
{
	public static class IBallStateFactory{
		public static IBallState CreateState(BallStates state, GameObject ball){
			switch(state){
				case BallStates.Moving:
					return new StateMoving(ball);
				case BallStates.outOfBounds:
					return new StateOutOfBounds(ball);
				default:
					return null;
			}			
		}
	}
}
