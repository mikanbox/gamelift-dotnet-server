#
## Local Run
### Run Local Server
java -jar GameLiftLocal.jar

### sample
dotnet run

### manual
mcs sample.cs -r:net6.0/GameLiftServerSDKNet45.dll -r:net6.0/Google.Protobuf.dll -r:net6.0/log4net.dll -r:net6.0/Newtonsoft.Json.dll -r:net6.0/System.Configuration.ConfigurationManager.dll -r:net6.0/websocket-sharp.dll
mono sample.exe

## Upload
dotnet build -o build
cd build
aws gamelift upload-build --name dotnetmanual2 --build-version 2 --build-root ./ --operating-system AMAZON_LINUX_2 --region ap-northeast-1


