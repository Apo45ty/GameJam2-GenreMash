using System.Collections;
using System.Collections.Generic;
using MLAPI.NetworkVariable;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
public class ScoreManager : NetworkBehaviour
{
    public NetworkVariableInt leftTeamScore = new NetworkVariableInt(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        },0);
    public NetworkVariableInt rightTeamScore = new NetworkVariableInt(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        },0);
    [SerializeField]
    private Text redTeamScoreBoard;
    [SerializeField]
    private Text blueTeamScoreBoard;

    // Update is called once per frame
    void Update()
    {
        if(redTeamScoreBoard)
            redTeamScoreBoard.text=""+rightTeamScore.Value;
        if(blueTeamScoreBoard)
            blueTeamScoreBoard.text=""+leftTeamScore.Value;
    }

    public void addScoreToBlueTeam(int score){
        leftTeamScore.Value+=score;
    }
    
    public void addScoreToRedTeam(int score){
        rightTeamScore.Value+=score;
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
