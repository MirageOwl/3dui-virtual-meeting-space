using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Logger : MonoBehaviour
{
    private int collisionCount;
    private List<TimeSpan> checkpointTimes;
    private DateTime start;
    private bool isDisabled = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        this.OnStartLogging();
        this.checkpointTimes = new List<TimeSpan>();
    }

    // Update is called once per frame
    private void Update()
    {
        //maybe change this later don't know how this is supposd to wrk
        var checkpointsLeft = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (!isDisabled && checkpointsLeft.Length == 0)
        {
            OnEndLogging();
        }
    }

    public void OnEndLogging()
    {
        isDisabled = true;
        Log("josh", "1", this.checkpointTimes.Take(5).ToList(), this.checkpointTimes.Skip(5), collisionCount);
    }

    public void OnStartLogging()
    {
        this.start = DateTime.Now;
    }

    public void EnterCheckpoint()
    {
        checkpointTimes.Add(DateTime.Now-start);
    }

    public void OnCollide()
    {
        collisionCount++;
    }

    private IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
    }

    public void Log(string participantID, string permutation, IEnumerable<TimeSpan> checkpointTimes0,
        IEnumerable<TimeSpan> checkpointTimes1, int collisionCount)
    {
        var json = $@"{{
                    ""timestamp"": {DateTimeOffset.Now.ToUnixTimeSeconds()},
                    ""participant_id"" : ""{participantID}"",
                    ""permutation"" : ""{permutation}"",
                    ""checkpoint_times_0"" : [{string.Join(", ", checkpointTimes0.Select(x => '"' + x.TotalSeconds + '"'))}],
                    ""checkpoint_times_1"" : [{string.Join(", ", checkpointTimes1.Select(x => '"' + x.TotalSeconds + '"'))}],
                    ""collision_count"" : {collisionCount} 
                }}
        ";
        StartCoroutine(Post("https://3dui-logs.azurewebsites.net/logParticipation", json));
    }
}