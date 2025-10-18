## customizacion de gestor de identidades
dotnet aspnet-codegenerator identity -dc DondeComemos.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout;Account.ForgotPassword;Account.ResetPassword"


Opcional del NUGET
dotnet tool install --global dotnet-aspnet-codegenerator --version 9.0.0

## EF migrations
dotnet ef migrations add AddRestaurante --context dondecomemos.Data.ApplicationDbContext -o
"D:\Usmp6\Programacion\DondeComemos\Data\Migrations"

No tener Ef
dotnet tool install --global dotnet-ef

actualizar 
dotnet ef database update

dotnet ef migrations add AddContacto --context dondecomemos.Data.ApplicationDbContext -o
"D:\Usmp6\Programacion\DondeComemos\Data\Migrations"
