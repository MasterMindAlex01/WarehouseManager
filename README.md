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
Cloud Deployments with Terraform to AWS, Docker Concepts, CICD Pipelines & Workflowsetc.

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
    
De forma predeterminada, la solución está configurada para funcionar con la base de datos MSSQL endocker. Por lo tanto, 
deberá asegurarse de que la instancia de la base de datos MSSQL se ejecute en el comando de docker-compose esté activa y 
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
