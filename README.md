# Arkhen Manufacturing

## Project Description

A full-stack application in which a user can register and log in, view and place orders to different store locations.

## Technologies Used

* .NET - Version 5.0.0
* ASP.NET Core MVC - Version 5.0.0
* Entity Framework Core - Version 5.0.0
* HTML, CSS, JavaScript, and Bootstrap
* Microsoft Azure App Service
* Microsoft Azure SQL Database
* Microsoft Azure DevOps (Pipeline specifically)
* SonarCloud for Static Code Analysis

## Features

* Customers can:
  * Register and create a user
  * Log out and Log in
  * View products from different store locations
  * Place products with different counts in a cart
  * Place an order to a location
  * View previous orders

* Admin Users
  * Can log in and log out
  * View orders placed to a location
  * Search Customers by their names (first, last, or a substring of the two)

To-do list:
* Implement more testing
* Improve the appearance of the application

## Getting Started
   
To get started with the application, clone the repository
(This also presumes you have git installed. If not, look towards [git-scm.com](https://git-scm.com/downloads))
> git clone https://github.com/2011-nov02-net/damien-project1

[ASP.NET Core Runtime 5.0.0](https://dotnet.microsoft.com/download/dotnet/5.0)

>Building from the command line:
* Navigate to the folder that was cloned, and into the 'ArkhenManufacturing' folder
* Navigate to the 'ArkhenManufacturing.WebApp' folder
* Run 'dotnet run' in the command line

>Building with Visual Studio (Windows/MacOS):
* Navigate to the parent folder that was cloned in a file explorer
* Open the 'ArkhenManufacturing' folder
* Open the 'ArkhenManufacturing.sln' file with Visual Studio

## Usage

> After cloning and running, the home page will be visible:
![](/image/home.png)

> From there, you can register if you haven't visited the site before
![](/image/register.png)

> And log in after registering
![](/image/login.png)

> Products can be viewed
![](/image/product_view.png)

> Products can be added to a cart
![](/image/cart.png)

> The user can place their order and view their orders here
![](/image/orders.png)

## License

This project uses the following license: [MIT License](https://github.com/git/git-scm.com/blob/master/MIT-LICENSE.txt).

