using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using TMPro;
using System.Linq;
using System;
public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instanssi;

    [SerializeField] public string scoreData;

    public TMP_Text[] names;
    public TMP_Text[] times;

    //List<string, string> allNames = new List<string, string>();
    //private var allNames = new List<(string, int)>();
    List<(string, string)> allNames = new List<(string, string)>{};
    List<int> allTimes = new List<int>();

    public TMP_Text Username_field;
    public TMP_Text User_time;
    private Ennatysajat EnnatysaikaScript;
    void Start()
    {
        instanssi = this;
        StartCoroutine(GetData());
        StartCoroutine(PutData("test1", 12, "67eea43a18ab20ea324a715d"));
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
    IEnumerator PutData(string dataStr, int time, string id) {
        string json = "{\"username\":\"" + dataStr + "\", \"time\":" + time + "}";
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        UnityWebRequest www = new UnityWebRequest("https://leaderboard-ictpaattotyo.onrender.com/api/scores/" + id, "PUT");   
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

        }
        www.Dispose();
        
    }

    private void placeData(List<MyJsonObject> jsonData)
    {
        jsonData.Sort((a, b) => a.time.CompareTo(b.time));
        for (int i = 0; i < jsonData.Count; i++)
        {
            var item = jsonData[i];
            //Debug.Log("Username: " + item.username + ", Time: " + item.time);
            allNames.Add((item.username, item.id));
            allTimes.Add(item.time);
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
    for (int i = 0; i < 2; i++) {
        Debug.Log(allNames[i]);
        Debug.Log(allNames[i].Item1);
        Debug.Log(allNames[i].Item2);
    }
    if (PlayerPrefs.HasKey("BestTimeLevel1") && PlayerPrefs.HasKey("BestTimeLevel2") && PlayerPrefs.HasKey("BestTimeLevel3") ) {
        if (allNames.Any(item => item.Item1 == Username_field.text.ToString())) {
            Debug.Log("Put");
            var id = allNames.FirstOrDefault(item => item.Item1 == Username_field.text.ToString()).Item2;
            StartCoroutine(PutData(Username_field.text.ToString(), (int)EnnatysaikaScript.Yhteisaika, id));
        } else {
            Debug.Log("POSt");
            StartCoroutine(PostData(Username_field.text.ToString(), (int)EnnatysaikaScript.Yhteisaika));
        }
    }
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
