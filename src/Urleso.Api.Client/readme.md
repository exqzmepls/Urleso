How to generate `Urleso API` client code:

1. Open your terminal in `src/Urleso.Api/` directory
2. Execute following command

```shell
dotnet build -c Release /p:ClientGen=True
```

Generated `Urleso API` client code will appear in `Urleso.Api.Client` project
