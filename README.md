# <img src="https://user-images.githubusercontent.com/59380623/90326583-a90f8b00-df4f-11ea-9037-f7898a415c1b.png" width="40">  **Constructora**
Aplicación web para almacenar archivos, desarrollado en .NET Core 3.1, ajax, bootstrap, jquery, jquery-validate, fontawesome, sweetalert.

## 1. Instalación
Asignar una conexión válida en appsettings.json

```hcl
"ConnectionStrings": {
   "ConstructoraConnection": "SERVER=Servidor;Database=BaseDatos;User=Usuario;Password=Contraseña;"
 }
```

Ejecutar los siguientes comandos en Package Manager Console

```hcl
Add-Migration InitialMigration

Update-Database
```

Crear un tipo de usuario y un usuario en la base de datos, para acceder a la aplicación web.

## 2. Ejemplo
Proyecto ejecutandose.

![image](https://user-images.githubusercontent.com/59380623/90326761-ef65e980-df51-11ea-9791-b67ebefb7869.png)

## 3. Licencia
**[MIT LICENSE](https://github.com/victorpaucas/Constructora/blob/master/LICENSE)**

Copyright © 2020 Victor Hugo Paucas Navarro
