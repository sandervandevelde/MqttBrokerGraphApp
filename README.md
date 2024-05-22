# EventGrid namespace MQTT support, topics per client overview

Demonstrator with an overview of Azure EventGrid namespace device clients and their topics.

## Walk-through

This [blog post](https://sandervandevelde.wordpress.com/2024/05/21/eventgrid-namespace-mqtt-support-topics-per-client-overview/) offers you a detailed walk-through on how to use this repo. 

## Configuration

Change the settings so the application has access to the right resources:  

```
string subscriptionId = "de442b11-d1e5-4655-aa8f-84011ad41d44";
string resourceGroupName = "iotgrid-demo-rg";
string namespaceName = "iotgrid-basic-egns";
```

Notice three environment variables are used to read the values from our system:

```
mqtt-graph-subscriptionid
mqtt-graph-resourcegroupname
mqtt-graph-namespacename
```

An alternative for the namespace name is available when the [MQTT client extensions](https://github.com/Azure-Samples/MqttApplicationSamples/tree/main/mqttclients) are used:

```
Environment.GetEnvironmentVariable("MQTT_HOST_NAME").Split('.').First()
```

Next to this access to the Azure Portal / Arm resources is needed.

There are two possibilities demonstrated for retrieving credentials:

```
// Use host application access
ArmClient client = new ArmClient(new DefaultAzureCredential());

// Use CLI access via AZ LOGIN 
var cred = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeSharedTokenCacheCredential = true });
ArmClient client = new ArmClient(cred);
```

The first line makes use of the Azure portal/arm access via the credentials used by the hosting application (eg. Visual Studio or an Azure Function).

The last two lines make use of the Azure portal/arm access via credentials stored in the Azure CLI.

login on the Dos prompt via:

```
az login
```

The result looks like this (this can take a couple of seconds or more to be constructed):

![image](https://github.com/sandervandevelde/MqttBrokerGraphApp/assets/694737/dbc411b5-3018-4f4d-8f75-4d801b84f361)

## Unit tests

A custom parser for the device client query had to be written.

The unit tests proof the support for a limited number of scenarios:

```
// attributes.type IN ['audit']
// attributes.type IN ['audit', 'a', 'AA',99]
// attributes.type in ['audit']
// attributes.type in ['audit', 'a', 'AA',99]
// attributes.type = "audit"
// attributes.type != "audit"
// attributes.type <> "audit"
```

So, it cannot work with integers or '> >= < <= and or parenthesis'.

Feel free to contribute for better query support.
