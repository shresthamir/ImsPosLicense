FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet build

EXPOSE 1070/tcp
ENV ASPNETCORE_URLS http://*:1070
ENV ASPNETCORE_ENVIRONMENT docker

ENTRYPOINT dotnet run