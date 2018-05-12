# Typewriter recipes

This is part of the presentation how to generate automaticaly models and services from controllers and model classes in .net core web api for angular>=4.3.x using [Typewriter](https://frhagn.github.io/Typewriter/)

## Backend
Original template for Web Api in Visual Studio is "too fat". It contains dependencies for MVC application. In following sample I use Andrew Lock template named [NetEscapades.Templates](https://github.com/andrewlock/NetEscapades.Templates) described in blog post [Removing the MVC Razor dependencies from the Web API template in ASP.NET Core](https://andrewlock.net/removing-the-mvc-razor-dependencies-from-the-web-api-template-in-asp-net-core/).
Additionaly static files serving capability was added.
```cs
    app.UseMvc();
    app.UseDefaultFiles(); //added
    app.UseStaticFiles(); //added
```

## Frontend
For frontend I initially used Angular 4.3.x - now upgraded to 6.0.1.
Dev environment for frontend is prepared with help of [Node Version Manager](https://github.com/coreybutler/nvm-windows).
```
nvm install 8.11.1
nvm use 8.11.1
```

Basic set of npm packages
```
npm i -g typescript
npm i -g tslint
npm i -g webpack
npm i -g @angular/cli@latest
npm i -g rxjs-tslint
```

For new project i use yarn as a package manager
```
ng set --global packageManager=yarn
```

Frontend folder is created in main folder of webapi .
For styles scss is used.
```
ng new ClientApp --style=scss
```
