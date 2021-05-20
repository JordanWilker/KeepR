dotnet publish -c Release
docker build -t keepr .
docker tag keepr registry.heroku.com/keepr-130/web
docker push registry.heroku.com/keepr-130/web
heroku container:release web -a keepr-130
echo press any key
read end