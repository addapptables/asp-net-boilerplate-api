FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY . .
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
EXPOSE 80/tcp
RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh