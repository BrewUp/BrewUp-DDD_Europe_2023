# BrewUp-DDD_Europe_2023

Before running the project, you need to start the services by running the following docker command in `./src/` folder :

    docker compose up

After that you can run the projects from visual studio or visual studio code

### From Visual Studio Code

To execute the `purchase` service, enter folder `purchases` and run the following command to start the project:

    dotnet run --project .\Brewup.Purchases.Rest\

after that, open a browser and go to `http://localhost:5218/swagger` to see the swagger documentation and test the endpoints

to run tests: 
  
    dotnet test

Same thing for the warehouse service, enter folder `warehouse` and run the following command to start the project:

    dotnet run --project .\Brewup.Warehouse.Rest\

after that, open a browser and go to `http://localhost:5043/swagger` to see the swagger documentation and test the endpoints

to run tests: 
  
    dotnet test


