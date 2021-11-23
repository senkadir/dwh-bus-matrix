FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

RUN mkdir -p /Dwh.Common/ 
RUN mkdir -p /Dwh.Core/ 
RUN mkdir -p /Dwh.Data/ 
RUN mkdir -p /Dwh.Domain/ 
RUN mkdir -p /Dwh.UI/ 

COPY ./src/Dwh.Common/. ./Dwh.Common
COPY ./src/Dwh.Core/. ./Dwh.Core
COPY ./src/Dwh.Data/. ./Dwh.Data
COPY ./src/Dwh.Domain/. ./Dwh.Domain
COPY ./src/Dwh.UI/. ./Dwh.UI

RUN dotnet publish ./Dwh.UI/ -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet","Dwh.UI.dll"]