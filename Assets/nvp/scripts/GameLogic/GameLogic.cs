using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace newvisionsproject.zong.gamelogic
{
	public static class DirectionLogic
	{
		public static Vector3 GetRandomDirection()
		{

			var v2 = Random.insideUnitCircle;
			var v3 = new Vector3(v2.x, 1f * Mathf.Sign(Random.value -0.5f), 0f).normalized;
			return v3;
		}

		public static Vector3 CalcRandomBounceFromPlayer(Vector3 movingDirection)
		{
			movingDirection.y = Mathf.Sign(movingDirection.y) * -1;
      movingDirection.x = Random.Range(-2.0f, 2.0f);
      movingDirection.Normalize();  
			return movingDirection;
		}		
	}
}