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

        - docker-compose up --build

 ![image](https://github.com/user-attachments/assets/baec7741-7a87-4b2c-a7d4-799484e9d325)
 ![image](https://github.com/user-attachments/assets/0371eb9d-9d85-4341-a28d-0c8af19f1058)
 ![image](https://github.com/user-attachments/assets/eedef84a-482e-4316-b079-28503e31deb7)
 ![image](https://github.com/user-attachments/assets/8839cdfe-bb4b-493d-b713-d937f1879fca)


    
De forma predeterminada, la solución está configurada para funcionar con la base de datos MSSQL endocker. Por lo tanto, 
deberá asegurarse de que la instancia de la base de datos MSSQL se ejecute en el comando de docker-compose esté activa y 
ejecutándose en su máquina. Puede modificar la cadena de conexión para incluir su nombre de usuario y contraseña. 
Las cadenas de conexión se pueden encontrar en src/Host/Configurations/database.json y src/Host/Configurations/hangfire.json. 
Una vez hecho esto, iniciemos el servidor API. para detener el servicios de docker

    - docker-compose down


Eso es todo, la aplicación se conectará a la base de datos MSSQL definida en docker y comenzará a crear tablas y a agregar los datos necesarios.

Para probar esta API, tenemos 2 opciones.

-@Swagger localhost:5001/swagger
- Las colecciones de POSTMAN están disponibles en ./postman




