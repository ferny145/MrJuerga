# MrJuerga
Minisite de venta de productos

Para utilizar este api es necesario se necesita la versión 2.2.300 de net core, esto se puede descargar del siguiente link <br />
https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.2.300-windows-x64-installer

Para poder realizar las migraciones a la base de datos es necesario ubicarse en el carpeta de Repository, esto se realiza utilizando estos comandos <br />
En primer lugar ejecutar el script MrJuerga.bak. en una base de datos sql 2018 o 2014<br />
En segundo lugar  escribir cd poner un espacio y presionando la tecla tab hasta encontrar el que dice Repository y finalmente se presiona enter <br />
En tercer lugar, poner el siguiente código dotnet ef --startup-project ../MrJuerga.Api database update "jwt register" <br />
<br />
Para poder ejecutar el api es necesario salir de la carpeta Repository para eso hay que ejecutar los siguientes comandos <br />
Primero, escribir cd .. <br />
En segundo lugar, escribir dotnet run


