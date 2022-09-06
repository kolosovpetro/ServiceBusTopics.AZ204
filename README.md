# Azure Service Bus Topics

This is demo in context of my preparation to the AZ204 exam.
It is all about to show azure service bus with topics straightforward and simple as possible.

## Infrastructure provision

- Create specified resource group `az group create --name "service-bus-topics-rg" --location "westus"`
- Create service bus
  namespace: `az servicebus namespace create --name "pkolosovsbnamespace" --resource-group "service-bus-topics-rg"`
- Create service bus
  queue: `az servicebus queue create --name "pkolsovqueue" --namespace-name "pkolosovsbnamespace" --resource-group "service-bus-topics-rg"`
- Create service bus
  topic: `az servicebus topic create --namespace-name "pkolosovsbnamespace" --name "testtopic" --resource-group "service-bus-topics-rg"`
- Get service bus connection string of
  format: `Endpoint=sb://pkolosovsbnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=`

## Application components

- Message sender (ASP NET API)
- Message consumer (ASP NET API)
- Service bus

## Required Nuget Packages

- `dotnet add package Azure.Messaging.ServiceBus`

