using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int leftTeamScore = 0;
    [SerializeField]
    private int rightTeamScore = 0;
    [SerializeField]
    private Text redTeamScoreBoard;
    [SerializeField]
    private Text blueTeamScoreBoard;

    // Update is called once per frame
    void Update()
    {
        if(redTeamScoreBoard)
            redTeamScoreBoard.text=""+rightTeamScore;
        if(blueTeamScoreBoard)
            blueTeamScoreBoard.text=""+leftTeamScore;
    }

    public void addScoreToBlueTeam(int score){
        leftTeamScore+=score;
    }
    
    public void addScoreToRedTeam(int score){
        rightTeamScore+=score;
    }

    public void addScoreToTeam(int score, Teams team){
        switch(team){
            case Teams.Red:
                addScoreToRedTeam(score);
                break;
            case Teams.Blue:
                addScoreToBlueTeam(score);
                break;
        }
    }
}
public enum Teams{
    Red,
    Blue
}
