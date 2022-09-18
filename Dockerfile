FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

COPY ./Workout.API/* ./Workout.API/
COPY ./Workout.Application/* ./Workout.Application/
COPY ./Workout.Core/* ./Workout.Core/
COPY ./Workout.Infrastructure/* ./Workout.Infrastructure/

RUN dotnet clean ./Workout.API
RUN dotnet restore ./Workout.API
RUN dotnet publish ./Workout.API -o /app/WorkoutAPI

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/WorkoutAPI /app

ENTRYPOINT [ "dotnet", "/app/Workout.API.dll" ]
