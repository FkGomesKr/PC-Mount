# Attention: This file is not intended to be used in Windows
all: web
web: dev-web
desktop: dev-desktop
build: build-web
db: build-db
relatorio: build-relatorio

# Launch profiles from "./Properties/launchSettings.json"
dev-desktop:
# Runs the two commands in parallel
# powershell -Command "Start-Process 'dotnet' -ArgumentList 'run' -WorkingDirectory './PCMount' -NoNewWindow"
	@powershell -Command "Start-Process 'dotnet' -ArgumentList 'run' -WorkingDirectory './PCMount'"
	@powershell -Command "Start-Process 'dotnet' -ArgumentList 'run' -WorkingDirectory './BlazorWinForms'"
dev-web:
	@cd ./PCMount/ && dotnet watch run -q
dev-web-https:
# Runs the default "https" profile in development mode
	@cd ./PCMount/ && dotnet run --launch-profile "https"
dev-web-http:
	@cd ./PCMount/ && dotnet run --launch-profile "http"
dev-clean:
	@cd ./PCMount/ && dotnet clean
build-web:
	@cd ./PCMount/ && dotnet run --configuration Release

# Dotnet Entity Framework
rand=%RANDOM%
build-db:
	@cd ./PCMount/ && if exist "./Migrations" rd /s /q "./Migrations" || true
	@cd ./PCMount/ && dotnet ef database drop --force --msbuildprojectextensionspath build/obj/
	@cd ./PCMount/ && dotnet ef migrations add $(rand) --msbuildprojectextensionspath build/obj/ && dotnet ef database update
	@cd ./PCMount/ && if exist "./obj" rd /s /q "./obj" || true
	@cd ./PCMount/ && if exist "./Migrations" rd /s /q "./Migrations" || true
clean-db:
	@cd ./PCMount/ && dotnet ef database drop --force

# Relatorio
build-relatorio:
	@cd Relatorio && typst compile main.typ
watch-relatorio:
	@cd Relatorio && typst watch main.typ
clean-relatorio:
	@if exist "Relatorio\main.pdf" del /f /q "Relatorio\main.pdf" || true