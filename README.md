# Sistema Oficial Mercy Developer

Sistema funcional Mercy Developer para la generación de ficha tecnica en el área de soporte Técnico

## Comenzando 🚀

_Las siguientes instrucciones les permitirá obtener una copia del proyecto para seguir con su desarrollo._

Mira **Deployment** para conocer como desplegar el proyecto.


### Pre-requisitos 📋

_Que cosas necesitas para instalar el software y como instalarlas_

```
1. .NET Core 8.0 + 
2. Visual Studio 2021+
3. WOrkBench 6.3+. Modelo de base de datos relacional
4. XAMPP para el ambiente local de base de datos MYSQL
5. Sincronizar la base de datos, el nombre de la DB es: "mercy_developer" 
```

### Ejecución del proyecto 🔧

_Si realizaste todo lo anterior (Pre-requisitos) puedes ejecutar el proyecto siguiendo estos pasos

_Se debe Ejecutar en terminal:_

```
1. Clona el repositorio con: git clone link del repositorio
```
_Para abrir el proyecto_
```
2. Localiza y ejecuta el archivo que esta dentro de tu proyecto clonado: MercDevs_ej2.sln
```
_Prueba o ejecuta el proyecto que este funcionando_
```
3. Depura el proyecto y prueba todos los módulos del mismo
```

_Si no se crean los registros con la DB, revisa el archivo en la instrucción ConnectionStrings en appsettings.json_

```
"ConnectionStrings": {
  "connection": "server=localhost;port=3306;database=mercy_developer;uid=root"
}
```
Crea un usuario en la base de datos y agrega la siguiente contraseña hasheada (contraseña plana: test): $2a$11$bL/hbgVmAwxXkdRwgjDebOct1/cxrLQyTYI7fm202QZyv5YdzPmHm
```
comando actualizar modelos nuevos en base de datos en el proyecto .NET .net "Scaffold-DbContext "server=localhost; port=3306; database=mercy_developer; uid=root; password=;" Pomelo.EntityFrameworkCore.MySql -o Models -context MercyDeveloperContext -force"
```

<!-- ## Ejecutando las pruebas ⚙️

_Explica como ejecutar las pruebas automatizadas para este sistema_

### Analice las pruebas end-to-end 🔩

_Explica que verifican estas pruebas y por qué_

```
Da un ejemplo
```

### Y las pruebas de estilo de codificación ⌨️

_Explica que verifican estas pruebas y por qué_

```
Da un ejemplo
``` -->

## Despliegue 📦

_Aún no esta listo el sistema para desplegarlo en producción (Web)_

## Construido con 🛠️

_Menciona las herramientas que utilizaste para crear tu proyecto_

* [.NET Core 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0) - El framework web usado
* [Visual Studio](https://visualstudio.microsoft.com/es/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false) - Codificar el proyecto
* [WorkBench 8.0](https://dev.mysql.com/downloads/workbench/) - Para la sincronización de la base de datos

## Contribuyendo 🖇️

Por favor lee el [El Profe](https://cftaricainformatica.cl) 

## Fortalece tu aprendizaje 📖

Invierte tiempo en tu formación profesional informática, accede a cualquier curso disponible y compeltalos [Cursos Gratis](https://www.udemy.com/courses/search/?price=price-free&q=.net+core+asp+web&sort=relevance&src=ukw)

## Versionado 📌

Usamos [SemVer](http://semver.org/) para el versionado. Para todas las versiones disponibles, mira los [tags en este repositorio](https://github.com/tu/proyecto/tags).

## Autores ✒️

_Menciona a todos aquellos que ayudaron a levantar el proyecto desde sus inicios_

* **Reinaldo Ordoñez** - *Trabajo Inicial* - [Reinaldo GitHub](https://github.com/reinaldo)
* **Felipe Castillo** - *Avances * - [Felipe GitHub](https://github.com/felipecastillo-b)
* **Edgardo Cayo** - *Documentación* - [cayocft](https://github.com/cayocft)


## Licencia 📄

Este proyecto está bajo la Licencia _FREE_ - mira el archivo [LICENSE.md](LICENSE.md) para detalles

## Expresiones de Gratitud 🎁

* Comenta a otros sobre este proyecto 📢
* Invita una cerveza 🍺 o un café ☕ a alguien del equipo. 
* Da las gracias públicamente 🤓.








