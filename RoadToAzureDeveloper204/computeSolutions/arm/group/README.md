# commands
## AZ CLI
```bash
az group create -n costa-road-204-compute -l westeurope
```

## ARM Template
```bash
az deployment sub create --location westeurope \
    --template-file .\arm\group\template.json \
    --parameters .\arm\group\parameters.json
```