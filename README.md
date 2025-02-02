# PCMount
This project was developed for the subject LI4 from University of Minho - Software Engineering degree.

#### Requirements:
To run this project you might need to install some of its dependencies:
- [.NET 8.0 (or latest)](https://dotnet.microsoft.com/en-us/)
- [Microsoft Blazor Fluent UI](https://www.fluentui-blazor.net/CodeSetup)
- [Microsoft SQL Server 2022 (or latest)](https://www.microsoft.com/en-us/sql-server/sql-server-2022) (Note: if you don't have a SQL Server license you can use the [Developer Edition](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))
- (Optional: to empirically/visually keep track of the database content) [Azure Data Studio](https://learn.microsoft.com/pt-pt/azure-data-studio/download-azure-data-studio)
- (Optional: used to compile the report) [Typst](https://typst.app/)

## How to install
```shell
$ git clone
$ cd ./UM-pcmount/
# After downloading the SQL Server Setup run the following command (example for SQL Server 2022 Developer Edition):
$ sudo ./path/SQL2022-SSEI-Dev.exe /ConfigurationFile="ConfigurationFile.ini"
```

###### After using the SQL Server Setup you can access the SQL Server Management Studio (SSMS) and use your Windows credentials to login or use the following credentials for development purposes:
##### Server name: `localhost\PCMOUNTSQL`
##### User: `sa`
##### Password: `root`

###### After setting up the SQL Server you need to create the database and tables by running the following commands:
```shell
$ cd ./PCMount/
# Install the Entity Framework Core tools by running:
$ dotnet tool install --global dotnet-ef
# Create the database by running:
$ dotnet ef migrations add InitialCreate
$ dotnet ef database update
# or:
$ cd ..
$ make db
```

##### Note: You can also use the SQL Server Management Studio (SSMS) to create the database and tables by running the SQL scripts found in the `./PCMount/Data/Scripts` folder.

###### Note: If you experience troubles with the NuGet packages run these commands:
```shell
cd ./PCMount/
$ dotnet add package Microsoft.FluentUI.AspNetCore.Components
$ dotnet add package Microsoft.FluentUI.AspNetCore.Components.Icons
$ dotnet add package Microsoft.FluentUI.AspNetCore.Components.Emoji
$ dotnet add package Microsoft.EntityFrameworkCore.SqlServer
$ dotnet add package Microsoft.EntityFrameworkCore.Tools
```

## How to build
```shell
$ make build
```

## How to run
```shell
$ make
```
or:
```shell
$ make dev
```

## Report
The pre-compiled version of the report can be found in the `Relatorio` folder (`main.pdf`). If you want to compile it yourself, you can do so by running:
```shell
$ make relatorio
```
###### Attention: You need to have [Typst](https://typst.app/) installed to compile

### Developement Team
Pedro Gomes