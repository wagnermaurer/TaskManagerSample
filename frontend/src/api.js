const API_BASE = "http://localhost:5108/api";

async function request(path, options = {}) {
  const response = await fetch(`${API_BASE}${path}`, {
    headers: { "Content-Type": "application/json" },
    ...options,
  });
  if (!response.ok) {
    throw new Error(`Request failed: ${response.status} ${response.statusText}`);
  }
  if (response.status === 204) return null;
  return response.json();
}

const TasksApi = {
  getAll: () => request("/tasks"),
  create: (title) => request("/tasks", { method: "POST", body: JSON.stringify({ title }) }),
  toggle: (id) => request(`/tasks/${id}/toggle`, { method: "PATCH" }),
  remove: (id) => request(`/tasks/${id}`, { method: "DELETE" }),
};
