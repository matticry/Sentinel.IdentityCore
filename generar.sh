#!/bin/bash

# Colores para output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Solicitar nombre del proyecto
echo -e "${BLUE}🏗️  Generador de Proyecto .NET 9.0 - Clean Architecture DDD${NC}"
echo ""
read -p "Ingresa el nombre del proyecto (ej: MiProyecto): " projectName

# Validar que no esté vacío
if [ -z "$projectName" ]; then
    echo -e "${YELLOW}❌ El nombre del proyecto no puede estar vacío${NC}"
    exit 1
fi

# Variables
solutionName="$projectName"
srcDir="src"
testDir="test"

echo -e "${GREEN}📁 Creando estructura de carpetas...${NC}"
mkdir -p "$srcDir"
mkdir -p "$testDir"

echo -e "${GREEN}📦 Creando solución .NET 9.0...${NC}"
dotnet new sln -n "$solutionName"

echo -e "${GREEN}🚀 Creando proyectos...${NC}"
dotnet new webapi -n "$solutionName.Api" -o "$srcDir/$solutionName.Api" -f net9.0
dotnet new classlib -n "$solutionName.Application" -o "$srcDir/$solutionName.Application" -f net9.0
dotnet new classlib -n "$solutionName.Domain" -o "$srcDir/$solutionName.Domain" -f net9.0
dotnet new classlib -n "$solutionName.Infrastructure" -o "$srcDir/$solutionName.Infrastructure" -f net9.0
dotnet new xunit -n "$solutionName.Tests" -o "$testDir/$solutionName.Tests" -f net9.0

echo -e "${GREEN}📦 Instalando paquetes NuGet para PostgreSQL...${NC}"
# Entity Framework Core para PostgreSQL
dotnet add "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" package Microsoft.EntityFrameworkCore.Design

# Paquetes adicionales útiles
dotnet add "$srcDir/$solutionName.Application/$solutionName.Application.csproj" package FluentValidation
dotnet add "$srcDir/$solutionName.Application/$solutionName.Application.csproj" package MediatR

echo -e "${GREEN}🔗 Agregando referencias entre proyectos...${NC}"
# Application depende de Domain
dotnet add "$srcDir/$solutionName.Application/$solutionName.Application.csproj" reference \
           "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj"

# Infrastructure depende de Application y Domain
dotnet add "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj" reference \
           "$srcDir/$solutionName.Application/$solutionName.Application.csproj" \
           "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj"

# Api depende de Application e Infrastructure
dotnet add "$srcDir/$solutionName.Api/$solutionName.Api.csproj" reference \
           "$srcDir/$solutionName.Application/$solutionName.Application.csproj" \
           "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"

# Tests referencian todo
dotnet add "$testDir/$solutionName.Tests/$solutionName.Tests.csproj" reference \
           "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj" \
           "$srcDir/$solutionName.Application/$solutionName.Application.csproj" \
           "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"

echo -e "${GREEN}📌 Agregando proyectos a la solución...${NC}"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Api/$solutionName.Api.csproj"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Application/$solutionName.Application.csproj"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Domain/$solutionName.Domain.csproj"
dotnet sln "$solutionName.sln" add "$srcDir/$solutionName.Infrastructure/$solutionName.Infrastructure.csproj"
dotnet sln "$solutionName.sln" add "$testDir/$solutionName.Tests/$solutionName.Tests.csproj"

echo -e "${GREEN}📂 Creando estructura de carpetas DDD...${NC}"
# Domain
mkdir -p "$srcDir/$solutionName.Domain/Entities"
mkdir -p "$srcDir/$solutionName.Domain/ValueObjects"
mkdir -p "$srcDir/$solutionName.Domain/Events"
mkdir -p "$srcDir/$solutionName.Domain/Exceptions"
mkdir -p "$srcDir/$solutionName.Domain/Repositories"

# Application
mkdir -p "$srcDir/$solutionName.Application/Commands"
mkdir -p "$srcDir/$solutionName.Application/Queries"
mkdir -p "$srcDir/$solutionName.Application/DTOs"
mkdir -p "$srcDir/$solutionName.Application/Interfaces"
mkdir -p "$srcDir/$solutionName.Application/Validators"

# Infrastructure
mkdir -p "$srcDir/$solutionName.Infrastructure/Persistence"
mkdir -p "$srcDir/$solutionName.Infrastructure/Persistence/Configurations"
mkdir -p "$srcDir/$solutionName.Infrastructure/Repositories"

echo -e "${GREEN}📝 Creando archivos .gitkeep para visualizar carpetas...${NC}"
# Domain
touch "$srcDir/$solutionName.Domain/Entities/.gitkeep"
touch "$srcDir/$solutionName.Domain/ValueObjects/.gitkeep"
touch "$srcDir/$solutionName.Domain/Events/.gitkeep"
touch "$srcDir/$solutionName.Domain/Exceptions/.gitkeep"
touch "$srcDir/$solutionName.Domain/Repositories/.gitkeep"

# Application
touch "$srcDir/$solutionName.Application/Commands/.gitkeep"
touch "$srcDir/$solutionName.Application/Queries/.gitkeep"
touch "$srcDir/$solutionName.Application/DTOs/.gitkeep"
touch "$srcDir/$solutionName.Application/Interfaces/.gitkeep"
touch "$srcDir/$solutionName.Application/Validators/.gitkeep"

# Infrastructure
touch "$srcDir/$solutionName.Infrastructure/Persistence/Configurations/.gitkeep"
touch "$srcDir/$solutionName.Infrastructure/Repositories/.gitkeep"

# Crear DbContext básico
cat > "$srcDir/$solutionName.Infrastructure/Persistence/ApplicationDbContext.cs" << EOF
using Microsoft.EntityFrameworkCore;

namespace $solutionName.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
EOF

# Crear appsettings.json con connection string de PostgreSQL
cat > "$srcDir/$solutionName.Api/appsettings.Development.json" << EOF
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=${solutionName}Db;Username=postgres;Password=postgres"
  }
}
EOF

echo ""
echo -e "${GREEN}✅ Proyecto '$solutionName' generado exitosamente con:${NC}"
echo -e "   ${BLUE}✓${NC} Arquitectura limpia DDD"
echo -e "   ${BLUE}✓${NC} .NET 9.0"
echo -e "   ${BLUE}✓${NC} PostgreSQL (Npgsql.EntityFrameworkCore.PostgreSQL)"
echo -e "   ${BLUE}✓${NC} MediatR para CQRS"
echo -e "   ${BLUE}✓${NC} FluentValidation"
echo -e "   ${BLUE}✓${NC} Carpetas visibles con .gitkeep"
echo ""
echo -e "${YELLOW}📝 Próximos pasos:${NC}"
echo "   1. cd $solutionName"
echo "   2. Configurar connection string en src/$solutionName.Api/appsettings.Development.json"
echo "   3. dotnet ef migrations add InitialCreate --project src/$solutionName.Infrastructure --startup-project src/$solutionName.Api"
echo "   4. dotnet ef database update --project src/$solutionName.Infrastructure --startup-project src/$solutionName.Api"
echo "   5. dotnet run --project src/$solutionName.Api"
echo ""
echo -e "${BLUE}💡 Estructura creada:${NC}"
echo "   Domain/Entities, ValueObjects, Events, Exceptions, Repositories"
echo "   Application/Commands, Queries, DTOs, Interfaces, Validators"
echo "   Infrastructure/Persistence, Repositories"