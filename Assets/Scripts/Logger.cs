using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Logger : MonoBehaviour
{
    private List<TimeSpan> checkpointTimes;
    private int collisionCount;
    private bool isDisabled;
    private DateTime start;

    // Start is called before the first frame update
    private void Start()
    {
        checkpointTimes = new List<TimeSpan>();
    }

    // Update is called once per frame
    private void Update()
    {
        //maybe change this later don't know how this is supposd to wrk
        // This check had moved to CheckpointOrder.cs script
        //var checkpointsLeft = GameObject.FindGameObjectsWithTag("Checkpoint");
        //if (!isDisabled && checkpointsLeft.Length == 0) OnEndLogging();
    }

    public void OnEndLogging()
    {
        isDisabled = true;
        Log(checkpointTimes.Take(5).ToList(), checkpointTimes.Skip(5), collisionCount);
        var splitUrl = Application.absoluteURL.Split('_');
        var splitUrl2 = splitUrl[1].Split('.');
        if (splitUrl[1].Contains("game2")) Application.OpenURL(splitUrl[0] + '_' + splitUrl2[0] + '.' + "postform");
        var controlType = splitUrl2[0];
        if (controlType.Contains('c'))
            Application.OpenURL(splitUrl[0] + "_d.middleform");
        else if (controlType.Contains('d')) Application.OpenURL(splitUrl[0] + "_c.middleform");
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