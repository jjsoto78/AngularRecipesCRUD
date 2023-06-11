# AngularRecipesCRUD
Angular CRUD Recipes project with Docker compose, Angular 15, C#, .Net Core 6, DDD

# Running this Web App
Locally copy this project's code, open a terminal and run

  git clone https://github.com/jjsoto78/AngularRecipesCRUD.git

There are two folders, angular-recipes and _AngularRecipesAPI.

Go into the folder angular-recipes.

Open a new terminal and run the command

  npm install

once done, run the command

  ng serve -o

Above's command should start the Angular Web project in a new browser window.

Now go to _AngularRecipesAPI/API folder

Open a new terminal and run the command

  dotnet watch run

Above's command should start the .Net REST WebAPI project. 
If any errors come up in the console try the command

  dotnet build

And then run again

  dotnet watch run

# Remarks
The WebAPI is using a sql Lite database for portability.
WebAPI's architecture is based off DDD.

# Docker
Docker compose files are included for each project and the whole app, if you have docker installed the comands to compose up or down are available, two containers will get instatiated.


