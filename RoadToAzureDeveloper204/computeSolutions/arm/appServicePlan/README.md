# commands
## AZ CLI
```bash
az group create -n costa-road-204-compute -l westeurope

az appservice plan create --name costa-road-204-appsrvplan-win \
    -g costa-road-204-compute \
    --sku F1

az appservice plan create --name costa-road-204-appsrvplan-linux \
    -g costa-road-204-compute \
    --sku F1 \
    --is-linux
```

## ARM Template
```bash
az deployment group create -g costa-road-204-compute \
    -f .\arm\appServicePlan\template.json \
    -p .\arm\appServicePlan\parameters.json
```
