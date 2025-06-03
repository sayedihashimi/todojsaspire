import { useState, useEffect } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [todos, setTodo] = useState([]);

  const getTodo = async ()=>{
    console.log("getting todo");
    fetch("/api/Todo")
      .then(response => response.json())
      .then(json => setTodo(json))
      .catch(error => console.error('Error fetching todos:', error));
  }

  useEffect(() => {
    getTodo();
  },[]);

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <h2>Todo list</h2>
      <ol>
        {todos.map(task => (
          <li key={task.id}>{task.title}</li>
        ))}
      </ol>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.jsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
