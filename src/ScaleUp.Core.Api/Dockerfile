FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base

EXPOSE 50000
EXPOSE 50001

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS prepare-restore-files
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset 
WORKDIR /src
COPY . .
RUN dotnet subset restore ScaleUp.sln --root-directory /src --output /src/restore_subset/

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS publish
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY --from=prepare-restore-files /src/restore_subset .
RUN dotnet restore "ScaleUp.sln" -a $TARGETARCH
COPY . .
WORKDIR "/src/ScaleUp.Core.Api"
RUN dotnet publish "ScaleUp.Core.Api.csproj" --no-restore -a $TARGETARCH  -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER $APP_UID  
ENTRYPOINT ["dotnet", "ScaleUp.Core.Api.dll"]