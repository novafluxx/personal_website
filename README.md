# Personal Website

This repository contains the code for my personal website.

## Overview
This repository contains the source code for my personal website built using Blazor WebAssembly. The website showcases my portfolio, contact information, and personal projects.

## Features
- Responsive Design: Adapts to various screen sizes.
- Experience Page: Showcases projects and professional background.
- Security Best Practices: Implements HTTP security headers, Content Security Policy, and input sanitization.
- Dark Mode Toggle: Uses `ThemeService` for theme switching.

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Blazor WebAssembly Tools

## Project Structure
- `personal_website.sln`: Visual Studio Solution file.
- `personal_website.csproj`: Project file defining dependencies and build settings.
- `Program.cs`: Application entry point, service registration, and startup logic.
- `appsettings.json`: Configuration settings.
- `Properties/`: Contains launch settings.
- `Components/`: Reusable Blazor UI components.
  - `Layout/`: Defines the overall page structure.
  - `Pages/`: Contains the routable pages/views (e.g., Home, Contact, Experience).
  - `UI/`: Smaller, general-purpose UI elements.
- `Services/`: Contains application logic services (e.g., `SecurityService`, `ThemeService`).
- `wwwroot/`: Static assets served directly to the client (CSS, JavaScript, images, `index.html`).
- `obj/`, `bin/`: Build output directories.

## Security
The project implements security best practices. Measures like HTTP header configuration and input sanitization are handled within `Services/SecurityService.cs` and configured during startup in `Program.cs`. The `SecurityService` also includes logic to *generate* a Content Security Policy (CSP); however, the application of this policy relies on appropriate `Content-Security-Policy` HTTP headers being set by the hosting server or platform.

## License
This project is licensed under the terms detailed in the LICENSE file.
