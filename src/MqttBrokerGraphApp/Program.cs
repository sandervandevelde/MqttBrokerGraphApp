﻿using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.EventGrid;
using Azure.ResourceManager.EventGrid.Models;
using DeviceClientQueryLibrary;

namespace MqttBrokerGraphApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("EventGrid MQTT broker - Client MQTT topics");
			Console.WriteLine("==========================================");

			// subscription id of the eventgrid namespace
			string? subscriptionId = Environment.GetEnvironmentVariable("mqtt-graph-subscriptionid");
			// name of the resource group
			string? resourceGroupName = Environment.GetEnvironmentVariable("mqtt-graph-resourcegroupname"); ;
			// name of the eventgrid namespace
			string? namespaceName = Environment.GetEnvironmentVariable("mqtt-graph-namespacename");

			// Use host application access
			//var cred = new DefaultAzureCredential();

			// Use CLI access via AZ LOGIN 
			var cred = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeSharedTokenCacheCredential = true });

			List<Client> clients = DeviceClientQueryProvider.GetClientTopics(subscriptionId, resourceGroupName, namespaceName, cred);

			foreach (Client client in clients) 
			{
				var enabledText = client.Enabled ? "Enabled" : "Disabled";

				Console.WriteLine($"Client {client.Name} ({enabledText})");

				foreach (var topic in client.Topics)
				{
					Console.WriteLine($"Can {topic.Usage} on: {topic.Name}");
				}

				Console.WriteLine();
			}
		}
	}
}
