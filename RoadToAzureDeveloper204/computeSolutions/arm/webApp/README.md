# commands
## AZ CLI
```bash
az group create -n costa-road-204-compute -l westeurope

az appservice plan create --name costa-road-204-appsrvplan -g costa-road-204-compute --sku F1 

az webapp create -g costa-road-204-compute -n costa-road-204-webapp -p costa-road-204-appsrvplan --runtime dotnetcore

```

## ARM Template
```bash
az deployment group create -g costa-road-204-compute --template-file .\arm\webApp\template.json --parameters .\arm\webApp\parameters.json
```
