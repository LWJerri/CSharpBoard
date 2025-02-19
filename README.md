# CSharpBoard

React ToDo Kanban with backend written on C#.

## Installation

1. Clone this repository - `git clone https://github.com/LWJerri/CSharpBoard.git`.

## Run Locally

### Without Docker

1. Install `.Net SDK` and all necessary tools (see [here](https://code.visualstudio.com/docs/csharp/get-started)).
2. Install [nvm](https://github.com/nvm-sh/nvm) or [LTS Node.js](https://nodejs.org) directly from official website.
3. Installed pnpm: `npm i pnpm@latest -g`.
4. Install all project dependencies - `pnpm i -r`.
5. Create `.env` file inside `apps/frontend` folder and paste environments from `.env.example` file.
6. Build frontend app - `pnpm frontend:build`.
7. Run frontend in _preview_ mode - `pnpm frontend:preview`.
8. Restore all .NET packages - `pnpm backend:restore`.
9. Run backend in development mode - `pnpm backend:dev`.

### With Docker

1. Create new `.env.demo` and paste all environments from `.env.demo.example` file.
1. Run `docker-compose -f docker-compose.demo.yaml --env-file=.env.demo up --build -d --remove-orphans`.

## License

This code has **MIT** license. See the `LICENSE` file for getting more information.
