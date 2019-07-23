# Check out the [sample RealWorld app](https://github.com/dnikolovv/dev-adventures-realworld) 

![template-in-vs](https://devadventures.net/wp-content/uploads/2018/06/template-in-vs.png)

#### To get familiar with the Maybe and Either monads you can take a look at the [introductory Option article](https://devadventures.net/2018/04/17/forget-object-reference-not-set-to-an-instance-of-an-object-functional-adventures-in-c/), [real life examples of Either in C#](https://devadventures.net/2018/09/20/real-life-examples-of-functional-c-sharp-either/) and the  [template introductory article](https://devadventures.net/2018/06/09/introducing-dev-adventures-net-core-project-template/).

---

## Features

### Web API (.NET Core 2.1)
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

![swagger-ui](https://devadventures.net/wp-content/uploads/2018/06/swagger-ui-new.png)

- [x] Global Model Errors Handler <br>

![model-errors](https://devadventures.net/wp-content/uploads/2018/05/model-errors.png)
- [x] Global Environment-Dependent Exception Handler <br>
Development <br>
![exception-development](https://devadventures.net/wp-content/uploads/2018/06/exception-development.png)<br> 
Production <br>
![enter image description here](https://devadventures.net/wp-content/uploads/2018/05/exception-production.png)
- [x] Neatly organized solution structure <br>
![solution-structure](https://devadventures.net/wp-content/uploads/2018/05/solution-structure.png)
- [x] Thin Controllers <br>
![thin-controllers](https://devadventures.net/wp-content/uploads/2018/05/tight-controllers.png) <br>
- [x] Robust service layer using the [Either](http://optional-github.com) monad. <br>
![either-monad](https://devadventures.net/wp-content/uploads/2018/05/either-monad.png)<br>
- [x] Safe query string parameter model binding using the [Option](http://optional-github.com) monad.<br>
![optional-binding](https://devadventures.net/wp-content/uploads/2018/05/option-binding-4-1.png)<br>

### Test Suite
- [x] xUnit
- [x] Autofixture
- [x] Moq
- [x] Shouldly
- [x] Arrange Act Assert Pattern

![enter image description here](https://devadventures.net/wp-content/uploads/2018/05/sample-test.png)