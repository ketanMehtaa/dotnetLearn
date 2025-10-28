below command will start the docker in detachable with ms sql 
docker compose up -d
# Create initial migration for SQL Server
dotnet ef migrations add InitialCreate

# Apply migration to create database
dotnet ef database update

# DBeaver Default

Host: localhost
Port: 1433
Database: master (or leave empty)
Username: sa
Password: data@123
