FROM microsoft/dotnet:sdk as builder
COPY . /app
WORKDIR /app
CMD dotnet publish -c Debug -o publish

FROM microsoft/dotnet:2.2-aspnetcore-runtime
COPY --from=builder /app/publish /app
WORKDIR /app
CMD dotnet MySimpleWebAPI.dll
