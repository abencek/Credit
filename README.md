
# CREDIT

ASP.NET Web application for financial companies offering lending products. Application can store, manage and process data about customers and their products.


## Technologies

- ASP.NET Core MVC
- Angular
- Bootstrap
- jQuery
- Chart.js
- Anime.js

## Features

- **ASP.NET Core Identity** implementation
- Dashboard with **Chart.js**
- Downloading to xlsx format with **OpenXML** library
- User profile settings page with **Angular**

## Screenshots

1. Application Sign in page
![Sign in](/.screenshots/Signin.png)

2. Page for viewing and editing data about customers
![Customers](/.screenshots/Customers.png)

3. Charts with some important metrics about customers
![Charts](/.screenshots/Charts.png)

4. Page for editing User profile
![User Profile](/.screenshots/UserProfile.png)

## Application setup before first run
1. Execute the following commands to apply migrations

  ```
    Update-Database -StartupProject Credit.Data -Project Credit.Data -Context AppDbContext
    Update-Database -StartupProject Credit.Data -Project Credit.Data -Context IdentityDbContext
  ```
2. Run the following SQL script to add initial data

    [AppDbSeedData.sql](/Credit.Data/AppDbSeedData.sql)

3. Use one of the following credentials to sign in

    Username | Password
    ------------ | -------------
    Admin1 | Admin
    User1 | User





