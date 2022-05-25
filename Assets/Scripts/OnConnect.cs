using System;
using System.Threading;

public class OnConnect
{
    public String[] Players;

    public void Start()
    {
        Thread.Sleep(1000);
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            foreach (String player in Players)
            {
                AddPlayer.CreatePlayer(player);
            }
        });
    }
}