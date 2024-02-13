# SelfieAWookie .NET Core 6 API

## Overview
This is a .NET Core 6 API project named "SelfieAWookie" that allows users to manage selfies and authentication.

## Table of Contents
1. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Installation](#installation)
2. [Usage](#usage)
   - [Selfie Controller](#selfie-controller)
   - [Authentication Controller](#authentication-controller)
3. [Contributing](#contributing)
4. [License](#license)

## Getting Started

### Prerequisites
- .NET SDK 6
- Visual Studio or Visual Studio Code (Optional)

### Installation
Clone the repository:

   ```bash
   git clone https://github.com/yourusername/SelfieAWookie.git
   cd SelfieAWookie
# Build and run the project:
dotnet run

# The API should now be running locally on https://localhost:5001.

# Usage

## Selfie Controller

### Get All Selfies:
```http
GET /api/V1/Selfie

##Add Picture:
POST /api/V1/Selfie/photos

##Add One Selfie:
POST /api/V1/Selfie

##Authentication Controller
Register:
POST /api/V1/Authenticate/register

##Login:
POST /api/V1/Authenticate

