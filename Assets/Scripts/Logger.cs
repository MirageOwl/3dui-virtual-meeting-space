using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Logger : MonoBehaviour
{
    private List<TimeSpan> checkpointTimes;
    private int collisionCount;
    private bool isDisabled;
    private DateTime start;

    private void Start()
    {
        checkpointTimes = new List<TimeSpan>();
    }
    
    [DllImport("__Internal")]
    private static extern void RedirectMe(); 

    public void OnEndLogging()
    {
        isDisabled = true;
        Log(checkpointTimes.Take(5).ToList(), checkpointTimes.Skip(5), collisionCount);
        RedirectMe();
    }

    public void OnStartLogging()
    {
        start = DateTime.Now;
    }

    public void EnterCheckpoint()
    {
        checkpointTimes.Add(DateTime.Now - start);
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

    private void Log(IEnumerable<TimeSpan> checkpointTimes0,
        IEnumerable<TimeSpan> checkpointTimes1, int collisionCount)
    {
        var split = Application.absoluteURL.Split('/');
        if (split.Length > 1)
        {
            var participantID = split[split.Length - 1];
            var permutation = split[split.Length - 2];
            var json = $@"{{
                                ""timestamp"": {DateTimeOffset.Now.ToUnixTimeSeconds()},
                                ""participant_id"" : ""{participantID}"",
                                ""permutation"" : ""{permutation}"",
                                ""checkpoint_times"" : [{string.Join(", ", checkpointTimes0.Select(x => '"' + x.TotalSeconds + '"'))}],
                                ""collision_count"" : {collisionCount} 
                            }}
                    ";
            StartCoroutine(Post("https://3dui-logs.azurewebsites.net/logParticipation", json));
        }
    }
}