import { useState, useEffect } from 'react';
import './TodoList.css';
import TodoItem from './TodoItem';

/**
 * Todo component represents the main TODO list application.
 * It allows users to add new tasks, delete tasks, and move tasks up or down in the list.
 * The component maintains the state of the task list and the new task input.
 */
function TodoList() {
    const [tasks, setTasks] = useState([]);
    const [newTaskText, setNewTaskText] = useState('');
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

    function handleInputChange(event) {
        setNewTaskText(event.target.value);
    }

    async function addTask(event) {
        if (newTaskText.trim()) {
            // call the API to add the new task
            const result = await fetch("/api/Todo", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ title: newTaskText, isCompleted: false })
            })
            if(result.ok){
                await getTodo();
            }
            // TODO: Add some error handling here, inform the user if there was a problem saving the TODO item.

            setNewTaskText('');
        }
        event.preventDefault();
    }

    async function deleteTask(id) {
        console.log(`deleting todo ${id}`);
        const result = await fetch(`/api/Todo/${id}`, {
            method: "DELETE"
        });

        if(result.ok){
            await getTodo();
        }
        // TODO: Add some error handling here, inform the user if there was a problem saving the TODO item.
    }

    async function moveTaskUp(index) {
        console.log(`moving todo ${index} up`);
        const todo = todos[index];
        const result = await fetch(`/api/Todo/move-up/${todo.id}`,{
            method: "POST"
        });

        if(result.ok){
            await getTodo();
        }
        else{
            console.error('Error moving task up:', result.statusText);
        }
    }

    async function moveTaskDown(index) {
        console.log(`moving todo ${index} up`);
        const todo = todos[index];
        const result = await fetch(`/api/Todo/move-down/${todo.id}`,{
            method: "POST"
        });

        if(result.ok){
            await getTodo();
        }
        else{
            console.error('Error moving task down:', result.statusText);
        }
    }

    return (
    <article
        className="todo-list"
        aria-label="task list manager">
        <header>
            <h1>TODO</h1>
                <form
                    className="todo-input"
                    onSubmit={addTask}
                    aria-controls="todo-list">
                <input
                    type="text"
                    required
                    autoFocus
                    placeholder="Enter a task"
                    value={newTaskText}
                    aria-label="Task text"
                    onChange={handleInputChange} />
                <button
                    className="add-button"
                    aria-label="Add task">
                    Add
                </button>
            </form>
        </header>
        <ol id="todo-list" aria-live="polite" aria-label="task list">
            {todos.map((task, index) =>
                <TodoItem
                    key={task.id}
                    task={task.title}
                    deleteTaskCallback={() => deleteTask(task.id)}
                    moveTaskUpCallback={() => moveTaskUp(index)}
                    moveTaskDownCallback={()=>moveTaskDown(index)}
                />
            )}
        </ol>
    </article>
    );
}

export default TodoList;