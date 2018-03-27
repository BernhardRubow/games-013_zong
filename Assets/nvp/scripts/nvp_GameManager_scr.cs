using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using newvisionsproject.managers.events;

public class nvp_GameManager_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	[SerializeField] int playerOneScrore;
	[SerializeField] int playerTwoScore;




	// +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private ScoringHelper scoringHelper;




	// +++ life cycle +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void Start () {

		scoringHelper = new ScoringHelper();

		// subscribe to events
		nvp_EventManager_scr.INSTANCE.SubscribeToEvent(GameEvents.onBallOutOfBounds, OnBallOutOfBounds);

	}
	
	void Update () {
		
	}




	// +++ event handler ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	void OnBallOutOfBounds(object sender, object eventArgs){
		float ballVerticalPosition = (float)eventArgs;
		int scoringPlayer = scoringHelper.GetScoringPlayerNo(ballVerticalPosition);
		ScorePlayer(scoringPlayer);
	}	

	void ScorePlayer(int playerNo){

		var playerScore = new PlayerScore();

    switch (playerNo)
    {
      case 1:
        playerOneScrore++;
				playerScore.PlayerNo = 1;
				playerScore.Score	= playerOneScrore;
        break;
      default:
        playerTwoScore++;
				playerScore.PlayerNo = 2;
				playerScore.Score	= playerTwoScore;
        break;
    }

		nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onPlayerScored, this, playerScore);
  }	
}




// +++ additional classes +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

public class ScoringHelper{
	public int GetScoringPlayerNo(float ballVerticalPosition){
		if(ballVerticalPosition > 0) return 1;
		return 2;
	}
}

public struct PlayerScore{
	public int PlayerNo;
	public int Score;
}
