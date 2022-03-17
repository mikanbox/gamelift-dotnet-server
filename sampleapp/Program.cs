using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using Aws.GameLift.Server;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/", () => {
    GameServer server = new GameServer();
    server.Start();

});

app.Run();


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