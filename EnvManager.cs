using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour
{
    public static EnvManager Instance { get; private set; }

    private Dictionary<string, string> env = new Dictionary<string, string>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadEnv();
    }

    void LoadEnv()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("env");
        if (jsonText == null)
        {
            Debug.LogError("env.json not found in Resources/");
            return;
        }

        EnvEntryList parsed = JsonUtility.FromJson<EnvWrapper>("{\"items\":" + jsonText.text + "}").ToList();
        env = parsed.ToDictionary();

        Debug.Log("✅ Env loaded (" + env.Count + " keys)");
    }

    public string Get(string key)
    {
        return env.ContainsKey(key) ? env[key] : null;
    }

    [System.Serializable]
    private class EnvEntry
    {
        public string key;
        public string value;
    }

    [System.Serializable]
    private class EnvWrapper
    {
        public EnvEntry[] items;

        public EnvEntryList ToList()
        {
            var list = new EnvEntryList();
            list.items = items;
            return list;
        }
    }

    private class EnvEntryList
    {
        public EnvEntry[] items;

        public Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in items)
            {
                dict[item.key] = item.value;
            }
            return dict;
        }
    }
}
