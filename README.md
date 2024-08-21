# WarehouseManager
Proyecto de almacén de productos con arquitectura limpia y soporte a docker y kubernetes

# Descripción general

El .NET Web API WarehouseManage es un punto de partida para Clean Architecture Project en .NET 8 que incorpora los paquetes 
y las características más esenciales que sus proyectos necesitarán, incluido el soporte multiusuario listo para usar. 
Este proyecto puede ahorrarle 200+ hoursa su equipo mucho más de tiempo de desarrollo.

# Objetivos 
El objetivo de este repositorio es proporcionar un punto de partida completo y rico en funciones para que cualquier desarrollador 
o equipo de .NET pueda poner en marcha su próximo gran proyecto utilizando la API web de .NET 8. Esto también sirve para aprender 
conceptos e implementaciones avanzados, como, por ejemplo, Multitenancy, CQRS, Onion Architecture, Clean Coding standards, 
Cloud Deployments with AKS (Azure Kubernetes Services), Docker Concepts, CICD Pipelines & Workflowsetc.

- [x] Desarrollado en .NET 8.0
- [x] Sigue los principios de arquitectura limpia
- [x] Diseño impulsado por el dominio
- [x] Listo para la nube. Se puede implementar en la infraestructura de AWS como contenedores ECS con Terraform.
- [x] Ejemplos de archivos Docker-Compose
- [x] Documentado
- [x] Soporte para múltiples inquilinos con Finbuckle
  - [x] Crear inquilinos con compatibilidad con múltiples bases de datos o bases de datos compartidas
  - [x] Activar/desactivar inquilinos a pedido
  - [x] Actualización de Suscripción de Inquilinos - ¡Agrega más meses de validez a cada inquilino!
- [x] ¡Compatible MSSQL con compatibilidad para otros proveedores --no estan incluido!

<details>
  <summary>¡Haga clic para ver más!</summary>
  
   - [x] Utiliza Entity Framework Core como abstracción de base de datos
   - [x] Patrón de repositorio flexible
   - [x] Integración elegante para un rendimiento óptimo
   - [x] Integración de Serilog con varios receptores: archivos, SEQ, Kibana
   - [x] OpenAPI: admite la generación de servicios al cliente
   - [x] Integración de Mapster para una cartografía más rápida
   - [x] Control de versiones de API
   - [x] Almacenamiento en caché de respuestas: almacenamiento en caché distribuido + REDIS
   - [x] Validaciones fluidas
   - [x] Registro de auditoría
   - [x] Gestión avanzada de permisos basados ​​en roles y usuarios
   - [x] Análisis de código e integración de StyleCop con conjuntos de reglas
   - [x] Localización basada en JSON con almacenamiento en caché
   - [x] Soporte de Hangfire: Panel de control seguro
   - [x] Servicio de almacenamiento de archivos
   - [x] Proyectos de prueba
   - [x] Autenticación JWT y Azure AD
   - [x] MediatR-CQRS
   - [x] Notificaciones de SignalR
   - [x] Y mucho más
</details>

# Empezando
Para comenzar con este projecto, aquí están las opciones disponibles:

# Development Environment

  - .NET SDK: 8.0
  - IDE: Visual studio 2022 o Visual studio Code
  - Base de datos: SQLServer - SQL Server Management Studio (SSMS)
  - API Testing Tools: POSTMAN
  - Contenedores: Docker
  - Kubernetes: MiniKube
  - Kubernetes-lens
    
# Guía de inicio rápido 
  - Cloné el repositorio WarehouseManager. Ahora que nuestra solución está generada, 
    naveguemos a la carpeta raíz de la solución y abramos una terminal de comandos para construir la solución.

         docker-compose up --build
    
De forma predeterminada, la solución está configurada para funcionar con la base de datos MSSQL en docker. Por lo tanto, 
deberá asegurarse de que la instancia de la base de datos MSSQL se ejecute en el comando de docker-compose  cuando esté activa y 
ejecutándose en su máquina. Puede modificar la cadena de conexión para incluir su nombre de usuario y contraseña. 
Las cadenas de conexión se pueden encontrar en src/Host/Configurations/database.json y src/Host/Configurations/hangfire.json. 
Una vez hecho esto, iniciemos el servidor API. para detener el servicios de docker

     docker-compose down


Eso es todo, la aplicación se conectará a la base de datos MSSQL definida en docker y comenzará a crear tablas y a agregar los datos necesarios.

Para probar esta API, tenemos 2 opciones.

- @Swagger localhost:5001/swagger
- Las colecciones de POSTMAN están disponibles en ./postman

Las credenciales predeterminadas para esta API son:

      {
        "email":"admin@root.com",
        "password":"123Pa$$word!"
      }
Abra Postman o Swagger.

identidad -> obtener token
Esta es una solicitud POST. Aquí, el cuerpo de la solicitud será el JSON (credenciales) que especifiqué anteriormente. Además, 
recuerda pasar el ID del inquilino en el encabezado de la solicitud. El ID del inquilino predeterminado es root.
A continuación se muestra un ejemplo de comando CURL para obtener los tokens.

    curl -X POST \
      'https://localhost:5001/api/tokens' \
      --header 'Accept: */*' \
      --header 'tenant: root' \
      --header 'Accept-Language: en-US' \
      --header 'Content-Type: application/json' \
      --data-raw '{
      "email": "admin@root.com",
      "password": "123Pa$$word!"
    }'
Y aquí está la respuesta.

    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM0YTY4ZjQyLWE0ZDgtNDNlMy1hNzE3LTI1OTczZjZmZTJjNyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQHJvb3QuY29tIiwiZnVsbE5hbWUiOiJyb290IEFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InJvb3QiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiQWRtaW4iLCJpcEFkZHJlc3MiOiIxMjcuMC4wLjEiLCJ0ZW5hbnQiOiJyb290IiwiaW1hZ2VfdXJsIjoiIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbW9iaWxlcGhvbmUiOiIiLCJleHAiOjE2ODA5NDE3MzN9.VYNaNvk2T4YDvQ3wriXgk2W_Vy9zyEEhjveNauNAeJY",
      "refreshToken": "pyxO30zJK8KelpEXF0vPfbSbjntdlbbnxrZAlUFXfyE=",
      "refreshTokenExpiryTime": "2023-04-15T07:15:33.5187598Z"
    }
¡Necesitarás pasarlo token en los encabezados de solicitud para autenticar las llamadas a la API WarehouseManager!

# Kubernetes: AKS (Azure Kubernetes Services)

Docker:
Antes de iniciar con Kubernetes, recordemos que estamos generando una imagen docker en el docker-compose up --build, esa 
imagen debemos publicarla en docker hub para que este disponible para usar desde los archivos yamel de kubernetes, 
nuevamente ubicandonos en la raiz del proyecto y usando el comando:

    docker push alexdrdeveloper01/warehousemanager-dotnet-webapi:latest

Notas: este es repositori publico si desea cambiar a un propio solo debe generar la imagen y publicarla en su docker hub y apuntarla en los archivos yaml de la carpeta /Kubernetes.

Continuamos con AKS:

[Lista de reproduccion para creacion de AKS por el portal](https://youtu.be/YlR9AkDJMMA?si=x8T47Yd3kIP9dd6k)

Crear un clúster de Kubernetes en Azure utilizando Azure Kubernetes Service (AKS) es un proceso relativamente sencillo 
y se puede hacer a través del portal de Azure, Azure CLI, o Terraform. A continuación, te guiaré a través del proceso 
utilizando Azure CLI, que es una de las formas más comunes y flexibles de hacerlo.

Prerrequisitos
  1) Cuenta de Azure: Necesitas una cuenta de Azure activa.
  2) Azure CLI: Asegúrate de tener instalada la última versión de Azure CLI. Puedes instalarla aquí.
Pasos para crear un clúster de Kubernetes en Azure

1) Iniciar sesión en Azure
Abre una terminal y ejecuta el siguiente comando para iniciar sesión en tu cuenta de Azure:

        az login

Esto abrirá una ventana del navegador donde podrás autenticarte.

2) Crear un grupo de recursos
Crea un grupo de recursos en la región donde deseas desplegar el clúster:

        az group create --name myResourceGroup --location eastus

3) Crear el clúster de AKS
Ejecuta el siguiente comando para crear un clúster de Kubernetes. Este ejemplo crea un clúster con un nodo.

        az aks create \
            --resource-group myResourceGroup \
            --name myAKSCluster \
            --node-count 1 \
            --enable-addons monitoring \
            --generate-ssh-keys

--resource-group: Especifica el grupo de recursos

--name: Asigna un nombre a tu clúster

--node-count: Define el número de nodos en el clúster

--enable-addons monitoring: Activa el monitoreo del clúster

--generate-ssh-keys: Genera llaves SSH si no tienes unas

4) Conectar con el clúster de AKS
Para administrar el clúster, debes configurar kubectl, que es la herramienta de línea de comandos para interactuar con Kubernetes.

        az aks get-credentials --resource-group myResourceGroup --name myAKSCluster
   
Este comando descarga las credenciales y configura el acceso al clúster para kubectl.

5) Verificar la instalación
Verifica que el clúster esté funcionando correctamente:

        kubectl get nodes


# Estructura del proyecto

Así es como está estructurado el Warehousemanager de .NET WebApi.

El .NET WebAPI Warehousemanager se basa en una arquitectura limpia. En otras palabras, arquitectura Onion/Hexagonal.

# Estructura general 

Esto significa que toda la solución está diseñada de tal manera que puede ser escalable y mantenida fácilmente por equipos de desarrolladores. Esta solución WebAPI consta principalmente de los siguientes archivos .csproj.


    ├── src
    |   |──Applications
    |   │   ├── api
    |   |   |   └── api.csproj
    │   ├── Core
    │   |   ├── Application.csproj
    │   |   ├── Shared.csproj
    │   |   └── Domain.csproj
    |   ├── Infrastructure
    |   |   └── Infrastructure.csproj
    |   ├── Migrators
    │   |   ├── Migrators.MSSQL.csproj
    │   |   ├── Migrators.MySQL.csproj
    │   |   ├── Migrators.PostgreSQL.csproj
    │   |   └── Migrators.Oracle.csproj

La idea es construir una arquitectura muy flexible siguiendo las mejores prácticas y paquetes. Veamos brevemente qué responsabilidades asume cada uno de estos proyectos.

# Applications 
Contiene los controladores de API y la lógica de inicio, incluida la configuración del contenedor ASP.NET Core. Este es el punto de entrada de la aplicación. Además, otros archivos estáticos como los registros, los archivos JSON de localización, las imágenes, las plantillas de correo electrónico y, lo más importante, los archivos de configuración se encuentran en este proyecto.

Con el lanzamiento de la versión, el archivo appSettings.json se divide en subconfiguraciones variables como database.json, security.json, etc., para lograr una mejor modularidad y organización. Puede encontrar estos nuevos archivos JSON en la carpeta Configuraciones del proyecto Host.

    ├── Api
    |   ├── Configurations
    |   ├── Controllers
    |   ├── Email Templates
    |   ├── Extensions
    |   ├── Files
    │   |   ├── Images
    │   |   └── Documents
    |   ├── Localization
    |   ├── Logs
    |   └── appsettings.json
    
Tenga en cuenta que el proyecto Host depende de

- Application
- Infrastructure
- Migration Projects

Este es uno de los proyectos de la carpeta principal, además del proyecto de dominio. Aquí puede ver las clases abstractas y las interfaces que se heredan e implementan en el proyecto de infraestructura. Esto hace referencia a la inversión de dependencias.

    ├── Core
    |   ├── Application
    |   |   ├── Auditing
    |   |   ├── Catalog
    |   |   ├── Common
    |   |   ├── Dashboard
    |   |   ├── Identity
    |   |   └── Multitenancy

Las carpetas se dividen en el nivel superior según las características. Esto significa que ahora es más fácil para los desarrolladores comprender la estructura de carpetas. Cada una de las carpetas de características, como Catálogo, tendrá todos los archivos relacionados con su ámbito, incluidos validadores, dtos, interfaces, etc.

De esta forma, todo lo relacionado con una función se encontrará directamente en esa carpeta de función.

En los casos en los que hay menos clases o interfaces asociadas a una función, todas estas clases se colocan directamente en la raíz de la carpeta de funciones. Solo cuando aumenta la complejidad de la función, se recomienda separar las clases por tipo.

Tenga en cuenta que el proyecto de aplicación depende únicamente de los proyectos principales, que son Sharedy Domain.

# Domain 
Tenga en cuenta que el proyecto de dominio no depende de ningún otro proyecto que no sea el Sharedproyecto.

Según los principios de la arquitectura limpia, el núcleo de esta solución, es decir, los proyectos de aplicación y de dominio, no dependen de ningún otro proyecto. Esto ayuda a lograr la inversión de dependencias (el principio "D" de "SOLID").
