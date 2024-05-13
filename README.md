# CSharpBoard

React ToDo Kanban with backend written on C#.

## Installation

1. Clone this repository - `git clone https://github.com/LWJerri/CSharpBoard.git`.

## Run Locally

### Without Docker

1. Install `.Net SDK` and all necessary tools (see [here](https://code.visualstudio.com/docs/csharp/get-started)).
2. Install [nvm](https://github.com/nvm-sh/nvm) or [LTS Node.js](https://nodejs.org) directly from official website.
3. Activate corepack (`corepack enable`) to enable pnpm or install it manually: `npm i pnpm@latest -g`.
4. Install all project dependencies - `pnpm i -r`.
5. Build frontend app - `pnpm frontend:build`.
6. Run frontend in _preview_ mode - `pnpm frontend:preview`.
7. Restore all .NET packages - `pnpm backend:restore`.
8. Run backend in development mode - `pnpm backend:dev`.

### With Docker

1. Create new `.env.demo` and paste all environments from `.env.demo.example` file.
1. Run `docker-compose -f docker-compose.demo.yaml --env-file=.env.demo up --build -d --remove-orphans`.

## License

This code has **MIT** license. See the `LICENSE` file for getting more information.
