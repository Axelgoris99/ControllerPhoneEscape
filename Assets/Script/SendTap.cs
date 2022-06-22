using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 
public class SendTap : NetworkBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Touch.OnTap += SendMsg;
        Touch.OnTap += DebugData;
    }

    void DebugData()
    {
        Debug.Log("Touched");
    }

    public struct ScoreMessage : NetworkMessage
    {
        public int score;
    }

    public void SendMsg()
    {
        ScoreMessage msg = new ScoreMessage()
        {
            score = 42,
        };

        NetworkServer.SendToAll(msg);
    }

    public void SetupClient()
    {
        NetworkClient.RegisterHandler<ScoreMessage>(OnScore);
        NetworkClient.Connect("localhost");
    }

    public void OnScore(ScoreMessage msg)
    {
        Debug.Log("OnScoreMessage " + msg.score);
    }
}
