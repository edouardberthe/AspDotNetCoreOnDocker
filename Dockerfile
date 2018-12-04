FROM microsoft/dotnet:sdk as builder
COPY . /app
WORKDIR /app
CMD dotnet publish -c Release -o publish Data

FROM microsoft/dotnet:runtime
COPY --from=builder /app/Data/publish /app
WORKDIR /app
CMD dotnet Data.dll
