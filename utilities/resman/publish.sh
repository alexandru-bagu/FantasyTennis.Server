dotnet publish -r linux-x64 --self-contained=false //p:PublishSingleFile=true --output output 
cp output/resman* ./
rm -rf output