using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System;
public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instanssi;

    [SerializeField] public string scoreData;

    public TMP_Text[] names;
    public TMP_Text[] times;

    public TMP_Text Username_field;
    public TMP_Text User_time;
    private Ennatysajat EnnatysaikaScript;
    void Start()
    {
        instanssi = this;
        StartCoroutine(GetData());
        EnnatysaikaScript = FindObjectOfType<Ennatysajat>();
    }



    IEnumerator PostData(string dataStr, int time) {
        string json = "{\"username\":\"" + dataStr + "\", \"time\":" + time + "}";
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
        StartCoroutine(GetData());
    }

    IEnumerator GetData() {
        UnityWebRequest www = UnityWebRequest.Get("https://leaderboard-ictpaattotyo.onrender.com/api/scores");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error: " + www.error);
        } else {
            string results = www.downloadHandler.text;
            Debug.Log("Server Response: "+ results);

            //MyJsonObject jsonObject = JsonUtility.FromJson<MyJsonObject>(results);
            MyJsonObjectList jsonObjectList = JsonUtility.FromJson<MyJsonObjectList>(results);
            
            // Nyt jsonObjectList.scores sisältää listan MyJsonObject-objekteista
            placeData(jsonObjectList.scores);

            
            //scoreData = results;
        }
        www.Dispose();
        
    }

    private void placeData(List<MyJsonObject> jsonData)
    {
        jsonData.Sort((a, b) => a.time.CompareTo(b.time));
        for (int i = 0; i < jsonData.Count; i++)
        {
            var item = jsonData[i];
            Debug.Log("Username: " + item.username + ", Time: " + item.time);

            // Näytetään tiedot UI:ssa
            if (i < names.Length && i < times.Length)
            {
                names[i].text = item.username;
                times[i].text = muunnaTekstiksi(item.time);
            } 
        }
    }

[System.Serializable]
public class MyJsonObject
{
        public string username;
        public int time;
        public string date;
        public string id;
        // Lisää kentät, jotka vastaavat JSON-objektin rakennetta
}

[System.Serializable]
public class MyJsonObjectList
{
    public List<MyJsonObject> scores;
}

public void Submit() {
    //Int32.Parse( User_time.text)
    StartCoroutine(PostData(Username_field.text.ToString(), (int)EnnatysaikaScript.Yhteisaika));
}
private string muunnaTekstiksi(int aika) {
        int tunnit = aika / 3600;
        int minuutit = (aika % 3600) / 60;
        int sekunnit = aika % 60;
        if (tunnit > 0) 
            return string.Format("{0:D2}:{1:D2}:{2:D2}", tunnit, minuutit, sekunnit);
    return string.Format("{0:D2}:{1:D2}", minuutit, sekunnit);
    }
}
