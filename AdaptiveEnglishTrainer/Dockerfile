#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdaptiveEnglishTrainer/AdaptiveEnglishTrainer.csproj", "AdaptiveEnglishTrainer/"]
COPY ["Component.TestCompetion.DAL/Component.TestCompletion.DAL.csproj", "Component.TestCompetion.DAL/"]
COPY ["Infrastructure.DAL/Infrastructure.DAL.csproj", "Infrastructure.DAL/"]
COPY ["Component.Groups.DAL/Component.Groups.DAL.csproj", "Component.Groups.DAL/"]
COPY ["Component.TestCompletion.BLL/Component.TestCompletion.BLL.csproj", "Component.TestCompletion.BLL/"]
COPY ["Component.TestManagement.BLL/Component.TestManagement.BLL.csproj", "Component.TestManagement.BLL/"]
COPY ["Component.TestManagement.DAL/Component.TestManagement.DAL.csproj", "Component.TestManagement.DAL/"]
COPY ["Component.Groups.BLL/Component.Groups.BLL.csproj", "Component.Groups.BLL/"]
COPY ["Component.Groups.PL/Component.Groups.PL.csproj", "Component.Groups.PL/"]
COPY ["Component.TestCompletion.PL/Component.TestCompletion.PL.csproj", "Component.TestCompletion.PL/"]
COPY ["Component.TestManagement.PL/Component.TestManagement.PL.csproj", "Component.TestManagement.PL/"]
RUN dotnet restore "AdaptiveEnglishTrainer/AdaptiveEnglishTrainer.csproj"
COPY . .
WORKDIR "/src/AdaptiveEnglishTrainer"
RUN dotnet build "AdaptiveEnglishTrainer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdaptiveEnglishTrainer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdaptiveEnglishTrainer.dll"]