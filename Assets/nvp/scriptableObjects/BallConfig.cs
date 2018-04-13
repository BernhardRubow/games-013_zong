using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Zong/Configuration/Ball")]
public class BallConfig : ScriptableObject {

	public float startSpeed;
	public float SpeedChange;
	public float TimeUntilNewBallStarts;
	public float rangeX;
	public float rangeY;
	
}
