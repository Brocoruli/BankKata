# BankKata

## Descripción
El repositorio "BankKata" alberga una aplicación C# desarrollada en .NET Core 6 que emplea una base de datos PostgreSQL a través de Entity Framework Core. El propósito principal de la kata es proporcionar tres endpoints que permitan realizar depósitos o retiros de dinero en una cuenta, así como verificar el balance de la misma.

## Requisitos Previos
Antes de ejecutar la aplicación, se recomienda tener instalado lo siguiente:

- .NET Core 6 SDK: Descargar e instalar
- Configuración de la Base de Datos
- PostgreSQL:

Asegúrate de que un servidor PostgreSQL esté en ejecución.
Configura la cadena de conexión en appsettings.json en el proyecto principal.

```
"ConnectionStrings": {
  "DefaultConnection": "Host=your_host;Port=your_port;Database=your_database;Username=your_username;Password=your_password;"
}
```

## Comandos Básicos de Migraciones
Creación de una Migración:
Para ejecutar los comandos de migración, sitúate en el terminal en el proyecto "BankKata".

Ejecuta el siguiente comando para crear una nueva migración.
```
dotnet ef migrations add initialMigrations --project BankKata.Infrastructure.Data --startup-project BankKata
```
Utiliza el siguiente comando para aplicar las migraciones pendientes a la base de datos.
```
dotnet ef database  update --project BankKata.Infrastructure.Data --startup-project BankKata
```
Revertir la Última Migración:
Si es necesario, puedes revertir la última migración ejecutando el siguiente comando.
```
dotnet ef migrations remove --project BankKata.Infrastructure.Data --startup-project BankKata
```