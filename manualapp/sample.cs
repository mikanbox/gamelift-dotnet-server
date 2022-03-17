using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using Aws.GameLift.Server;

class sample{
    static private GameServer server = null;
    
    public static void Main() {
        Console.WriteLine("Hello World");
        server = new GameServer();

        
        // server.Start();
    }
}

public class GameServer {
    public bool IsAlive {get; private set;} = false;
    public void Start(){
        IsAlive = true;

        var listeningPort = 7777;
        var initSDKOutcome = GameLiftServerAPI.InitSDK();
        if (initSDKOutcome.Success){ }
    }

    void OnApplicationQuit(){
        IsAlive = false;
        GameLiftServerAPI.ProcessEnding();
    }
}


