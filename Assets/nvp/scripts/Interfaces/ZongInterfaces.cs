using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace newvisionsproject.zong.interfaces{
	public interface IEffect 
	{
		void Play();
	}

	public interface IPlayerInput
	{
		float GetTargetXPosition();
	}
}