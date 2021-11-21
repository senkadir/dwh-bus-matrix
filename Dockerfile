FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

RUN mkdir -p /Matrix.Common/ 
RUN mkdir -p /Matrix.Core/ 
RUN mkdir -p /Matrix.Data/ 
RUN mkdir -p /Matrix.Domain/ 
RUN mkdir -p /Matrix.UI/ 

COPY ./src/Matrix.Common/. ./Matrix.Common
COPY ./src/Matrix.Core/. ./Matrix.Core
COPY ./src/Matrix.Data/. ./Matrix.Data
COPY ./src/Matrix.Domain/. ./Matrix.Domain
COPY ./src/Matrix.UI/. ./Matrix.UI

RUN dotnet publish ./Matrix.UI/ -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet","Matrix.UI.dll"]