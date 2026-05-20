function AddTaskForm({ onAdd }) {
  const [title, setTitle] = React.useState("");
  const [submitting, setSubmitting] = React.useState(false);

  async function handleSubmit(event) {
    event.preventDefault();
    const trimmed = title.trim();
    if (!trimmed) return;

    setSubmitting(true);
    try {
      await onAdd(trimmed);
      setTitle("");
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <form className="add-form" onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="What needs to be done?"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        disabled={submitting}
      />
      <button type="submit" disabled={submitting || !title.trim()}>
        Add
      </button>
    </form>
  );
}
