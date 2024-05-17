# MqttBrokerGraphApp

Demonstrator for reading the Azure EventGrid namespace device client graph

## Configuration

Change the settings so the application has access to the right resources:  

```
string subscriptionId = "de442b11-d1e5-4655-aa8f-84011ad41d44";
string resourceGroupName = "iotgrid-demo-rg";
string namespaceName = "iotgrid-basic-egns";
```

Next to this access to the Azure Portal / Arm resources is needed.

There are two possibilies shown:

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

The result looks like this:

![image](https://github.com/sandervandevelde/MqttBrokerGraphApp/assets/694737/3c41ab8e-9ab3-486e-849c-91d58e385117)

## Unit tests

A custom parser for the device client query had to be written.

The unittests proof the support for a limited number of scenarios:

```
		// attributes.type IN ['audit']
		// attributes.type IN ['audit', 'a', 'AA',99]
		// attributes.type = "audit"
		// attributes.type != "audit"
		// attributes.type <> "audit"
```

Feel free to contribute for a better query support.
