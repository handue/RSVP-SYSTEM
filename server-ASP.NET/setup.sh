#!/bin/bash

# 프로젝트 이름 설정
# Project Name
PROJECT_NAME="RSVP"

# 솔루션 생성
# Create solution
echo "Creating solution..."
dotnet new sln -n $PROJECT_NAME

# 프로젝트 생성
# Create projects
echo "Creating projects..."
dotnet new webapi -n "$PROJECT_NAME.API"
dotnet new classlib -n "$PROJECT_NAME.Core"
dotnet new classlib -n "$PROJECT_NAME.Infrastructure"
dotnet new xunit -n "$PROJECT_NAME.Tests"

# 솔루션에 프로젝트 추가
# Add projects to solution
echo "Adding projects to solution..."
dotnet sln add "$PROJECT_NAME.API/$PROJECT_NAME.API.csproj"
dotnet sln add "$PROJECT_NAME.Core/$PROJECT_NAME.Core.csproj"
dotnet sln add "$PROJECT_NAME.Infrastructure/$PROJECT_NAME.Infrastructure.csproj"
dotnet sln add "$PROJECT_NAME.Tests/$PROJECT_NAME.Tests.csproj"

# 프로젝트 간 참조 추가
# Add project references
echo "Adding project references..."
dotnet add "$PROJECT_NAME.API/$PROJECT_NAME.API.csproj" reference "$PROJECT_NAME.Core/$PROJECT_NAME.Core.csproj"
dotnet add "$PROJECT_NAME.API/$PROJECT_NAME.API.csproj" reference "$PROJECT_NAME.Infrastructure/$PROJECT_NAME.Infrastructure.csproj"
dotnet add "$PROJECT_NAME.Infrastructure/$PROJECT_NAME.Infrastructure.csproj" reference "$PROJECT_NAME.Core/$PROJECT_NAME.Core.csproj"
dotnet add "$PROJECT_NAME.Tests/$PROJECT_NAME.Tests.csproj" reference "$PROJECT_NAME.API/$PROJECT_NAME.API.csproj"
dotnet add "$PROJECT_NAME.Tests/$PROJECT_NAME.Tests.csproj" reference "$PROJECT_NAME.Core/$PROJECT_NAME.Core.csproj"
dotnet add "$PROJECT_NAME.Tests/$PROJECT_NAME.Tests.csproj" reference "$PROJECT_NAME.Infrastructure/$PROJECT_NAME.Infrastructure.csproj"

# NuGet 패키지 설치 (Similar to Node's npm install )
# Install NuGet packages
echo "Installing NuGet packages..."
cd "$PROJECT_NAME.API"
# SQL Server 데이터베이스 ORM
# SQL Server database ORM
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.5
# EF Core 마이그레이션 도구
# EF Core migration tools
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.5
# JWT 인증
# JWT authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.5
# 객체 매핑 도구
# Object mapping tool
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
# 입력 데이터 유효성 검사
# Input data validation
dotnet add package FluentValidation.AspNetCore --version 11.3.0
# API 문서화 도구
# API documentation tool
dotnet add package Swashbuckle.AspNetCore --version 6.5.0
cd ..

echo "Project setup completed!"