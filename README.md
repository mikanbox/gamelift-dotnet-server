#


## Run
### sample
dotnet run

### manual
mcs sample.cs -r:net6.0/GameLiftServerSDKNet45.dll -r:net6.0/Google.Protobuf.dll -r:net6.0/log4net.dll -r:net6.0/Newtonsoft.Json.dll -r:net6.0/System.Configuration.ConfigurationManager.dll -r:net6.0/websocket-sharp.dll
mono sample.exe

