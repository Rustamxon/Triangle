rmdir /s /q Triangle.Data\Migrations

echo y | dotnet ef database drop --project Triangle.Data --startup-project Triangle.Api

dotnet ef migrations add %random% --project Triangle.Data --startup-project Triangle.Api

dotnet ef database update --project Triangle.Data --startup-project Triangle.Api