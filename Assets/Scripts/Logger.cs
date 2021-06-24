using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Logger : MonoBehaviour
{
// Start is called before the first frame update
    private void Start()
    {
        Log("josh", "1",
            new[]
            {
                TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(40),
                TimeSpan.FromSeconds(50)
            }, new[]
            {
                TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(40),
                TimeSpan.FromSeconds(50)
            });
    }

// Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator Post(string url, string bodyJsonString)
    {
        var request = new UnityWebRequest(url, "POST");
        var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        Debug.Log("Status Code: " + request.responseCode);
    }

    public IEnumerator Log(string participantID, string permutation, IEnumerable<TimeSpan> checkpointTimes0,
        IEnumerable<TimeSpan> checkpointTimes1)
    {
        var json = $@"
                {{
                    ""timestamp"": DateTimeOffset.Now.ToUnixTimeSeconds()
                    ""participant_id"" : ""{participantID}"",
                    ""permutation"" : ""{permutation}"",
                    ""checkpoint_times_0"" : [{string.Join(", ", checkpointTimes0.ToList())}],
                    ""checkpoint_times_1"" : [{string.Join(", ", checkpointTimes1.Select(x => x.ToString()))}]
                }}
        ";
        return Post("https://3dui-logs.azurewebsites.net/logParticipation", json);
    }
}