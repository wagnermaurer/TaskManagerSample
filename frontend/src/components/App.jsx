function App() {
  const [tasks, setTasks] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState(null);

  React.useEffect(() => {
    (async () => {
      try {
        const data = await TasksApi.getAll();
        setTasks(data);
      } catch (e) {
        setError(e.message);
      } finally {
        setLoading(false);
      }
    })();
  }, []);

  async function handleAdd(title) {
    try {
      const created = await TasksApi.create(title);
      setTasks((prev) => [created, ...prev]);
    } catch (e) {
      setError(e.message);
    }
  }

  async function handleToggle(id) {
    try {
      const updated = await TasksApi.toggle(id);
      setTasks((prev) => prev.map((t) => (t.id === id ? updated : t)));
    } catch (e) {
      setError(e.message);
    }
  }

  async function handleDelete(id) {
    try {
      await TasksApi.remove(id);
      setTasks((prev) => prev.filter((t) => t.id !== id));
    } catch (e) {
      setError(e.message);
    }
  }

  return (
    <main className="app">
      <h1>Task Manager</h1>
      <AddTaskForm onAdd={handleAdd} />
      {loading && <p className="loading">Loading…</p>}
      {error && <p className="error">{error}</p>}
      {!loading && !error && (
        <TaskList tasks={tasks} onToggle={handleToggle} onDelete={handleDelete} />
      )}
      <Footer />
    </main>
  );
}
