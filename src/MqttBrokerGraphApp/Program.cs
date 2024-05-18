using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.EventGrid;
using Azure.ResourceManager.EventGrid.Models;
using DeviceClientQueryLibrary;

namespace MqttBrokerGraphApp
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("EventGrid MQTT broker - Client MQTT topics");
			Console.WriteLine("==========================================");

			// subscription id of the eventgrid namespace
			string? subscriptionId = Environment.GetEnvironmentVariable("mqtt-graph-subscriptionid");
			// name of the resource group
			string? resourceGroupName = Environment.GetEnvironmentVariable("mqtt-graph-resourcegroupname"); ;
			// name of the eventgrid namespace
			string? namespaceName = Environment.GetEnvironmentVariable("mqtt-graph-namespacename");

			await GetClientTopics(subscriptionId, resourceGroupName, namespaceName);

			await Task.Delay(1);
		}

		public static async Task GetClientTopics(string subscriptionId, string resourceGroupName, string namespaceName)
		{
			// Use host application access
			//ArmClient client = new ArmClient(new DefaultAzureCredential());

			// Use CLI access via AZ LOGIN 
			var cred = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeSharedTokenCacheCredential = true });
			ArmClient client = new ArmClient(cred);

			var eventGridNamespaceResourceId = EventGridNamespaceResource.CreateResourceIdentifier(subscriptionId, resourceGroupName, namespaceName);
			EventGridNamespaceResource eventGridNamespace = client.GetEventGridNamespaceResource(eventGridNamespaceResourceId);

			var collectionClients = eventGridNamespace.GetEventGridNamespaceClients();
			var allClients = collectionClients.GetAll();
			var listClients = allClients.ToList();

			var collectionClientGroups = eventGridNamespace.GetEventGridNamespaceClientGroups();
			var allClientGroups = collectionClientGroups.GetAll();
			var listClientGroups = allClientGroups.ToList();

			var collectionPermissionBindings = eventGridNamespace.GetEventGridNamespacePermissionBindings();
			var allPermissionBindings = collectionPermissionBindings.GetAll();
			var listPermissionBindings = allPermissionBindings.ToList();

			var collectionTopicSpaces = eventGridNamespace.GetTopicSpaces();
			var allTopicSpaces = collectionTopicSpaces.GetAll();
			var listTopicSpaces = allTopicSpaces.ToList();

			// all clients related to the query eg. " attributes.type IN ['audit'] "
			foreach (var clientInList in listClients)
			{
                await Console.Out.WriteLineAsync($"Client {clientInList.Data.Name}:");

                var clientInGroups = new List<EventGridNamespaceClientGroupResource>();

				var clientPublisherPermissingBindings = new List<EventGridNamespacePermissionBindingResource>();

				var clientSubscriberPermissingBindings = new List<EventGridNamespacePermissionBindingResource>();

				var clientPublisherTopicTemplates = new List<string>();

				var clientSubscriberTopicTemplates = new List<string>();

				foreach (var attribute in clientInList.Data.Attributes)
				{
					foreach (var clientGroup in listClientGroups)
					{
						if ((clientGroup.Data.Query.ToLower() == "true") 
								|| DeviceClientQueryHelper.Query(clientGroup.Data.Query, attribute))
						{
							if (!clientInGroups.Any(x => x.Id == clientGroup.Id))
							{
								clientInGroups.Add(clientGroup);
							}
						}
					}
				}
							
				foreach(var permissionBinding in listPermissionBindings)
				{
					var clientGroup = clientInGroups.FirstOrDefault(x => x.Data.Name == permissionBinding.Data.ClientGroupName);

					if (clientGroup != null)
					{
						if (permissionBinding.Data.Permission.Value == PermissionType.Subscriber)
						{
							if (!clientSubscriberPermissingBindings.Any(x => x.Id == permissionBinding.Id))
							{
								clientSubscriberPermissingBindings.Add(permissionBinding);
							}
						}

						if (permissionBinding.Data.Permission.Value == PermissionType.Publisher)
						{
							if (!clientPublisherPermissingBindings.Any(x => x.Id == permissionBinding.Id))
							{
								clientPublisherPermissingBindings.Add(permissionBinding);
							}
						}
					}
				}

				foreach (var clientPublisherPermissingBinding in clientPublisherPermissingBindings)
				{
					var topicSpace = listTopicSpaces.First(x => x.Data.Name == clientPublisherPermissingBinding.Data.TopicSpaceName);

					clientPublisherTopicTemplates.AddRange(topicSpace.Data.TopicTemplates);
				}

				foreach (var clientSubscriberPermissingBinding in clientSubscriberPermissingBindings)
				{
					var topicSpace = listTopicSpaces.First(x => x.Data.Name == clientSubscriberPermissingBinding.Data.TopicSpaceName);

					clientSubscriberTopicTemplates.AddRange(topicSpace.Data.TopicTemplates);
				}

				foreach(var x in clientPublisherTopicTemplates)
				{
					var y = x.Replace("${client.authenticationName}", clientInList.Data.Name);

					await Console.Out.WriteLineAsync($"Can publish on: {y}");
				}

				foreach (var x in clientSubscriberTopicTemplates)
				{
					var y = x.Replace("${client.authenticationName}", clientInList.Data.Name);

					await Console.Out.WriteLineAsync($"Can subscribe on: {y}");
				}

				await Console.Out.WriteLineAsync();
			}
		}
	}
}
