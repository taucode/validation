dotnet restore

dotnet build TauCode.Validation.sln -c Debug
dotnet build TauCode.Validation.sln -c Release

dotnet test TauCode.Validation.sln -c Debug
dotnet test TauCode.Validation.sln -c Release

nuget pack nuget\TauCode.Validation.nuspec