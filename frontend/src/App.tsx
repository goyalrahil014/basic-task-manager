import React, { useEffect, useState } from "react";
import { TaskItem } from "./types";
import { getTasks, createTask, updateTask, deleteTask } from "./api";

type Filter = "All" | "Active" | "Completed";

export default function App() {
  const [tasks, setTasks] = useState<TaskItem[]>([]);
  const [desc, setDesc] = useState("");
  const [loading, setLoading] = useState(false);
  const [filter, setFilter] = useState<Filter>("All");

  const load = async () => {
    setLoading(true);
    try {
      const items = await getTasks();
      setTasks(items);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
  }, []);

  const handleAdd = async (e?: React.FormEvent) => {
    e?.preventDefault();
    const trimmed = desc.trim();
    if (!trimmed) return;
    const created = await createTask({ description: trimmed, isCompleted: false });
    setTasks((s) => [...s, created]);
    setDesc("");
  };

  const toggle = async (t: TaskItem) => {
    const updated = { ...t, isCompleted: !t.isCompleted };
    await updateTask(t.id, updated);
    setTasks((s) => s.map((x) => (x.id === t.id ? updated : x)));
  };

  const remove = async (id: string) => {
    await deleteTask(id);
    setTasks((s) => s.filter((t) => t.id !== id));
  };

  const filtered = tasks.filter((t) =>
    filter === "All" ? true : filter === "Active" ? !t.isCompleted : t.isCompleted
  );

  return (
    <div className="app">
      <h1>Task Manager</h1>

      <form onSubmit={handleAdd} className="add-form">
        <input
          value={desc}
          onChange={(e) => setDesc(e.target.value)}
          placeholder="New task description"
        />
        <button type="submit">Add</button>
      </form>

      <div className="filters">
        {(["All", "Active", "Completed"] as Filter[]).map((f) => (
          <button
            key={f}
            onClick={() => setFilter(f)}
            className={f === filter ? "active" : ""}
          >
            {f}
          </button>
        ))}
      </div>

      {loading ? (
        <p>Loading...</p>
      ) : (
        <ul className="task-list">
          {filtered.map((t) => (
            <li key={t.id} className={t.isCompleted ? "completed" : ""}>
              <label>
                <input
                  type="checkbox"
                  checked={t.isCompleted}
                  onChange={() => toggle(t)}
                />
                <span className="desc">{t.description}</span>
              </label>
              <button className="delete" onClick={() => remove(t.id)}>
                Delete
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
