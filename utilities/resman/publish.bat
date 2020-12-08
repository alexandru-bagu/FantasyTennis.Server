dotnet publish -r win-x64 --self-contained=false /p:PublishSingleFile=true --output output 
copy output\resman.exe .\
rmdir /S /Q output