import axios from "axios";
import { TaskItem } from "./types";

const api = axios.create({
  baseURL: "http://localhost:5000/api",
  headers: { "Content-Type": "application/json" },
});

export const getTasks = async (): Promise<TaskItem[]> => {
  const res = await api.get<TaskItem[]>("/tasks");
  return res.data;
};

export const createTask = async (task: Partial<TaskItem>): Promise<TaskItem> => {
  const res = await api.post<TaskItem>("/tasks", task);
  return res.data;
};

export const updateTask = async (id: string, task: TaskItem) => {
  await api.put(`/tasks/${id}`, task);
};

export const deleteTask = async (id: string) => {
  await api.delete(`/tasks/${id}`);
};
