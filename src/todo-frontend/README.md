# Todo Frontend

This is the **frontend** for the Todo application, built with [React](https://react.dev/) and [Vite](https://vitejs.dev/). It provides a simple and interactive interface for managing a list of tasks, including adding, deleting, and reordering todos. The frontend communicates with a backend API to persist and update todo items.

## Features

- Add new tasks
- Delete existing tasks
- Move tasks up or down in the list
- Fetches and updates todos from a backend API (`/api/Todo`)
- Accessible and responsive UI

## Project Structure

```
todo-frontend/
├── public/
│   └── checkmark-square.svg
├── src/
│   ├── App.jsx
│   ├── App.css
│   ├── index.css
│   ├── main.jsx
│   ├── assets/
│   └── components/
│       ├── TodoList.jsx
│       └── TodoItem.jsx
├── index.html
├── package.json
├── vite.config.js
├── eslint.config.js
└── README.md
```

## Getting Started

### Prerequisites
- [Node.js](https://nodejs.org/) (v18 or higher recommended)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)

### Installation

1. Navigate to the `todo-frontend` directory:
   ```sh
   cd src/todo-frontend
   ```
2. Install dependencies:
   ```sh
   npm install
   # or
   yarn install
   ```

### Running the Development Server

Start the Vite development server:
```sh
npm run dev
# or
yarn dev
```

The app will be available at [http://localhost:5173](http://localhost:5173) by default.

### API Integration

This frontend expects a backend API to be available at `/api/Todo` for CRUD operations. Make sure the backend is running and accessible. The API endpoints used include:
- `GET /api/Todo` — fetch all todos
- `POST /api/Todo` — add a new todo
- `DELETE /api/Todo/{id}` — delete a todo
- `POST /api/Todo/move-up/{id}` — move a todo up
- `POST /api/Todo/move-down/{id}` — move a todo down

## Linting

To check code style and lint errors:
```sh
npm run lint
# or
yarn lint
```

## Build

To build the app for production:
```sh
npm run build
# or
yarn build
```

## License

This project is licensed under the terms of the [MIT License](../../LICENSE).
