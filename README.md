# MrJuerga
Minisite de venta de productos

Para utilizar este api es necesario se necesita la versión 2.2.300 de net core, esto se puede descargar del siguiente link <br />
https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.2.300-windows-x64-installer <br />

Para poder conectarse con su base de datos (SQL 2018) van a tener cambiar el archivo appsettings.json y modificar la parte que dice LAPTOP-260LOCSK\\MSSQLSERVER2017 y poner sus propias credenciales <br />


Para poder realizar las migraciones a la base de datos es necesario ubicarse en el carpeta de Repository, esto se realiza utilizando estos comandos <br />
En primer lugar ejecutar el script MrJuerga.bak.en la base de datos SQL 2018 <br />
En segundo lugar  escribir cd poner un espacio y presionando la tecla tab hasta encontrar el que dice Repository y finalmente se presiona enter <br />
En tercer lugar, poner el siguiente código dotnet ef --startup-project ../MrJuerga.Api database update "jwt register" <br />
<br />
Para poder ejecutar el api es necesario salir de la carpeta Repository para eso hay que ejecutar los siguientes comandos <br />
Primero, escribir cd .. <br />
En segundo lugar, escribir dotnet run


