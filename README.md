## ローカル環境で動作確認

1. git clone
2. WeatherApi のポート番号を確認して、もし変更されていたらBlazorWebAppのlaunchSettings.jsonを開き、"Services__weatherapi": "http://localhost:5289"のポート番号を直す

## ローカル環境でコンテナの動作確認をする

1. docker build .\BlazorWebApp\ -t kuradev/blazorweapp:latest
2. docker build .\WeatherApi\ -t kuradev/weatherapi:latest
3. docker run -d -it --rm -p 8080:8080 --name weatherapi kuradev/weatherapi:latest
4. docker run -d -it --rm -p 18080:8080 --name blazorwebapp -e Services__weatherapi='http://weatherapi:8080' kuradev/blazorwebapp:latest
5. ネットワークを作成してコンテナを接続（これをすることで、BlazorWebAppからコンテナ名で通信できる）
    docker network create my-network -d bridge
    docker network connect my-network weatherapi
    docker network connect my-network blazorwebapp

## Azure Container Registry にプッシュ

1. docker login ～～～.azurecr.io
2. docker tag kuradev/blazorweapp:latest ～～～.azurecr.io/blazorweapp:latest
3. docker tag kuradev/weatherapi:latest ～～～.azurecr.io/weatherapi:latest
4. docker push ～～～.azurecr.io/blazorweapp:latest
5. docker push ～～～.azurecr.io/weatherapi:latest

## Azure Container Apps にデプロイ

1. weatherapi コンテナへのデプロイはイングレスを8080にすることだけ注意
2. blazorwebapp コンテナへのデプロイ
    環境変数 Services__weatherapi に http://weatherapi をセット
    イングレスを8080にセット