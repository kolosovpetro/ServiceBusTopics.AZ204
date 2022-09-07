# Azure Service Bus Topics

[![Run Build and Test](https://github.com/kolosovpetro/ServiceBusTopics.AZ204/actions/workflows/run-build-and-test-dotnet.yml/badge.svg)](https://github.com/kolosovpetro/ServiceBusTopics.AZ204/actions/workflows/run-build-and-test-dotnet.yml)

This is demo in context of my preparation to the AZ204 exam.
It is all about to show azure service bus with topics straightforward and simple as possible.

## Infrastructure provision

- Switch to proper subscription: `az account set --subscription <name or id>`
- Create specified resource group `az group create --name "service-bus-topics-rg" --location "westus"`
- Create service bus
  namespace: `az servicebus namespace create --name "pkolosovsbnamespace" --resource-group "service-bus-topics-rg"`
- Create service bus
  queue: `az servicebus queue create --name "pkolsovqueue" --namespace-name "pkolosovsbnamespace" --resource-group "service-bus-topics-rg"`
- Create service bus topics:
    - `az servicebus topic create --namespace-name "pkolosovsbnamespace" --name "topic_one" --resource-group "service-bus-topics-rg"`
    - `az servicebus topic create --namespace-name "pkolosovsbnamespace" --name "topic_two" --resource-group "service-bus-topics-rg"`
- Create subscriptions:
    - `az servicebus topic subscription create --resource-group "service-bus-topics-rg" --namespace-name "pkolosovsbnamespace" --name "topic_one_subscription" --topic-name "topic_one"`
    - `az servicebus topic subscription create --resource-group "service-bus-topics-rg" --namespace-name "pkolosovsbnamespace" --name "topic_two_subscription" --topic-name "topic_two"`
- Get service bus connection string of
  format: `Endpoint=sb://pkolosovsbnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=`

## Application components

- Message sender (ASP NET API)
- Message consumer (ASP NET API)
- Service bus

## Required Nuget Packages

- `dotnet add package Azure.Messaging.ServiceBus`

