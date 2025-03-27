using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class Leaderboard : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetData());
        StartCoroutine(PostData("test"));   
    }

    IEnumerator PostData(string dataStr) {
        string json = "{\"username\":\"" + dataStr + "\", \"time\":4321}";
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
        //autentikaatiota varten :www.SetRequestHeader("Authorization", "Bearer YOUR_API_KEY");
        
        UnityWebRequest www = new UnityWebRequest("https://leaderboard-ictpaattotyo.onrender.com/api/scores", "POST");
        www.uploadHandler = new UploadHandlerRaw(jsonBytes);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        } else{
            string results = www.downloadHandler.text;
            Debug.Log(results);
        }
        www.Dispose();

    }

    IEnumerator GetData() {
        UnityWebRequest www = UnityWebRequest.Get("https://leaderboard-ictpaattotyo.onrender.com/api/scores");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error: " + www.error);
        } else {
            string results = www.downloadHandler.text;
            Debug.Log("Server Response: "+ results);
        }
        www.Dispose();
    }
}
