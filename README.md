This template is available in the [Visual Studio Marketplace](https://marketplace.visualstudio.com/items?itemName=dnikolovv.dev-adventures-project-setup#overview). Also make sure to check out the [sample RealWorld app](https://github.com/dnikolovv/dev-adventures-realworld).

After installing the VSIX you will see the template available under Visual C#:

![template-in-vs](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/template-in-vs.JPG)

> To get familiar with the Maybe and Either monads you can take a look at the [introductory Option article](https://devadventures.net/2018/04/17/forget-object-reference-not-set-to-an-instance-of-an-object-functional-adventures-in-c/), [real life examples of Either in C#](https://devadventures.net/2018/09/20/real-life-examples-of-functional-c-sharp-either/) and the  [template introductory article](https://devadventures.net/2018/06/09/introducing-dev-adventures-net-core-project-template/).


---

## Features

### Web API
- [x] AutoMapper
- [x] EntityFramework Core with SQL Server and ASP.NET Identity
- [x] JWT authentication/authorization
- [x] File logging with Serilog
- [x] Stylecop
- [x] Neat folder structure
```
├───src
│   ├───configuration
│   └───server
│       ├───MyProject.Api
│       ├───MyProject.Business
│       ├───MyProject.Core
│       ├───MyProject.Data
│       └───MyProject.Data.EntityFramework
└───tests
    └───MyProject.Business.Tests

```


- [x] Swagger UI + Fully Documented Controllers <br>

![swagger-ui](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/swagger-ui.JPG)

- [x] Global Model Errors Handler <br>

![model-errors](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/model-errors.JPG)
- [x] Global Environment-Dependent Exception Handler <br>
Development <br>
![exception-development](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/exception-development.JPG)<br> 
Production <br>
![enter image description here](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/exception-prod.JPG)
- [x] Neatly organized solution structure <br>
![solution-structure](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/solution-structure.JPG)
- [x] Thin Controllers <br>
![thin-controllers](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/thin-controllers.JPG) <br>
- [x] Robust service layer using the [Either](http://optional-github.com) monad. <br>
![either-monad](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/either-monad.JPG)<br>
- [x] Safe query string parameter model binding using the [Option](http://optional-github.com) monad.<br>
![optional-binding](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/optional-binding.JPG)<br>

### Test Suite
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly
- [x] Arrange Act Assert Pattern

![test-suite](https://raw.githubusercontent.com/dnikolovv/devadventures-net-core-template/master/images/test-suite.JPG)
