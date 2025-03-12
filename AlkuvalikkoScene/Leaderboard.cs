using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
   [SerializeField]
   private List<TextMeshProUGUI> names;
   [SerializeField]
   private List<TextMeshProUGUI> scores;
   private string publicLeaderboardKey = "70f3de7840a2836deb1b9fc4f98e86002ef4c417640d3df703b70e6cd8d1f752";

    private void Start()
    {
       GetLeaderboard();  
    }
    public void GetLeaderboard() {
    LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
        int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
        for (int i = 0; i < loopLength; i++) {
            names[i].text = msg[i].Username;
            scores[i].text = msg[i].Score.ToString();
        }
    }));
   }

   public void SetLeaderboard(string username, int score) {
    LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) => {
        GetLeaderboard();
    }));
   }
   
}
