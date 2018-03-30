using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using newvisionsproject.managers.events;

public class nvp_GameManager_scr : MonoBehaviour {

	// +++ inspector fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	
	[SerializeField] gameConfig gameConfig;




	// +++ private fields +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	private ScoringHelper scoringHelper;
	int playerOneScrore;
	int playerTwoScore;




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




	// +++ methods ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
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

		CheckForWinningCondition();

		nvp_EventManager_scr.INSTANCE.InvokeEvent(GameEvents.onPlayerScored, this, playerScore);
  }	

	void CheckForWinningCondition(){
		if(playerOneScrore >= gameConfig.WinningScore || playerTwoScore >= gameConfig.WinningScore){
			// possible winnig condition
			if(Mathf.Abs(playerOneScrore - playerTwoScore) >= 2){
				// one player has at least 2 or more point than the other
				if(playerOneScrore > playerTwoScore){
					SceneManager.LoadScene("playerOneWins");
				}
				else{
					SceneManager.LoadScene("playerTwoWins");
				}
			}
		}
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
