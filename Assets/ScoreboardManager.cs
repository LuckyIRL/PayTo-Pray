using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ScoreboardManager : MonoBehaviour
{
    // Static instance of the ScoreboardManager
    private static ScoreboardManager instance;
    public static ScoreboardManager Instance { get { return instance; } }

    public TMP_Text[] topTimesText; // Array of TextMeshPro text components to display the top times
    public TMP_InputField playerNameInput; // Reference to the UI input field for player name
    private const int MaxEntries = 10; // Maximum number of entries to display
    private const string TimeEntriesKey = "TimeEntries"; // PlayerPrefs key for storing time entries

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Retrieve time entries from PlayerPrefs
        List<TimeEntry> timeEntries = RetrieveTimeEntries();

        // Sort time entries by time taken (ascending order)
        timeEntries.Sort((a, b) => a.TimeTaken.CompareTo(b.TimeTaken));

        // Display the top 10 fastest times on the scoreboard
        DisplayTopTimes(timeEntries);
    }

    private List<TimeEntry> RetrieveTimeEntries()
    {
        // Retrieve time entries from PlayerPrefs
        string serializedData = PlayerPrefs.GetString(TimeEntriesKey, "");
        return DeserializeData(serializedData);
    }

    private void SaveTimeEntries(List<TimeEntry> timeEntries)
    {
        // Serialize time entries and store them in PlayerPrefs
        string serializedData = SerializeData(timeEntries);
        PlayerPrefs.SetString(TimeEntriesKey, serializedData);
        PlayerPrefs.Save(); // Save changes to PlayerPrefs immediately
    }

    public void UpdateScoreboard(string playerName, float elapsedTime)
    {
        // Retrieve time entries from PlayerPrefs
        List<TimeEntry> timeEntries = RetrieveTimeEntries();

        // Add a new time entry for the current player with the elapsed time and player name
        timeEntries.Add(new TimeEntry { PlayerName = playerName, TimeTaken = (long)(elapsedTime * 1000) }); // Convert to milliseconds

        // Sort time entries by time taken (ascending order)
        timeEntries.Sort((a, b) => a.TimeTaken.CompareTo(b.TimeTaken));

        // Save updated time entries to PlayerPrefs
        SaveTimeEntries(timeEntries);

        // Display the top times on the scoreboard
        DisplayTopTimes(timeEntries);
    }

    private void DisplayTopTimes(List<TimeEntry> timeEntries)
    {
        // Display up to the top 10 fastest times on the scoreboard
        int count = Mathf.Min(timeEntries.Count, MaxEntries);
        for (int i = 0; i < count; i++)
        {
            topTimesText[i].text = $"{i + 1}. {timeEntries[i].PlayerName}: {FormatTime(timeEntries[i].TimeTaken)}";
        }

        // If there are fewer than 10 entries, clear the remaining text fields
        for (int i = count; i < MaxEntries; i++)
        {
            topTimesText[i].text = "";
        }
    }

    private string FormatTime(long milliseconds)
    {
        // Convert milliseconds to a readable format (e.g., minutes:seconds)
        long minutes = milliseconds / 60000;
        long seconds = (milliseconds % 60000) / 1000;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private string SerializeData(List<TimeEntry> timeEntries)
    {
        // Serialize the list of TimeEntry objects into a JSON string
        return JsonUtility.ToJson(new TimeEntriesWrapper { Entries = timeEntries });
    }

    private List<TimeEntry> DeserializeData(string serializedData)
    {
        // Deserialize the JSON string into a list of TimeEntry objects
        TimeEntriesWrapper wrapper = JsonUtility.FromJson<TimeEntriesWrapper>(serializedData);
        return wrapper != null ? wrapper.Entries : new List<TimeEntry>();
    }

    internal void UpdateScoreboard(string playerName)
    {
        throw new System.NotImplementedException();
    }

    // Define a class to wrap the list of time entries for serialization
    [System.Serializable]
    private class TimeEntriesWrapper
    {
        public List<TimeEntry> Entries;
    }

    // Define a class or struct to represent a time entry
    [System.Serializable]
    public class TimeEntry
    {
        public string PlayerName;
        public long TimeTaken;
    }
}
