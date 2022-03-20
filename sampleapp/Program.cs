using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using Aws.GameLift.Server;
using log4net;
[assembly: log4net.Config.XmlConfigurator(ConfigFile="log4net.config",Watch=true)]


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

GameServer server = new GameServer();
server.Start();




app.MapGet("/", () => "Hello World!");
app.MapGet("/start", () => {
});
app.MapGet("/testconnect", () => {
    Console.WriteLine("testconnect");
    return "connected";
});
app.MapGet("/terminate", () => {
    server.ApplicationTerminate();
    Environment.Exit(0);
});

app.Run();




// InitSDK + ProcessReady
public class GameServer {
    public bool IsAlive {get; private set;} = false;
    static ILog logger = LogManager.GetLogger(typeof(Program));

    public void Start(){
        logger.Info("Start");

        IsAlive = true;
        var listeningPort = 7777;
        var initSDKOutcome = GameLiftServerAPI.InitSDK();
        if (initSDKOutcome.Success) {
            // https://docs.aws.amazon.com/ja_jp/gamelift/latest/developerguide/integration-server-sdk-csharp-ref-datatypes.html#integration-server-sdk-csharp-ref-dataypes-process
            ProcessParameters processParameters = new ProcessParameters(
                (gameSession) => {GameLiftServerAPI.ActivateGameSession();},
                (updateGameSession) => {},
                () => {GameLiftServerAPI.ProcessEnding();}, 
                () => {return true;},
                listeningPort, //This game server tells GameLift that it will listen on port 7777 for incoming player connections.
                new LogParameters(new List<string>() {
                    "/local/game/logs/myserver.log"
                }));

            // InitSDKが終わったことを GameLift サービスに通知する
            var processReadyOutcome = GameLiftServerAPI.ProcessReady(processParameters);
            if (processReadyOutcome.Success) {
                logger.Info("ProcessReady success.");
            } else {
                logger.Info("ProcessReady failure : " + processReadyOutcome.Error.ToString());
            }
        } else {
            logger.Error("InitSDK failure : " + initSDKOutcome.Error.ToString());
        }
    }

    public void ApplicationTerminate() {
        OnApplicationQuit();
    }

    void OnApplicationQuit(){
        IsAlive = false;
        GameLiftServerAPI.ProcessEnding();
    }

}