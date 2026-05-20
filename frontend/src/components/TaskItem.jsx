function TaskItem({ task, onToggle, onDelete }) {
  function handleDelete(event) {
    event.stopPropagation();
    onDelete(task.id);
  }

  const createdLabel = new Date(task.createdAt).toLocaleString(undefined, {
    dateStyle: "short",
    timeStyle: "short",
  });

  return (
    <li
      className={`task-item ${task.isCompleted ? "completed" : ""}`}
      onClick={() => onToggle(task.id)}
    >
      <input
        type="checkbox"
        checked={task.isCompleted}
        onChange={() => onToggle(task.id)}
        onClick={(e) => e.stopPropagation()}
      />
      <span className="task-title">{task.title}</span>
      <span className="task-created">{createdLabel}</span>
      <button
        type="button"
        className="task-delete"
        aria-label={`Delete ${task.title}`}
        onClick={handleDelete}
      >
        ×
      </button>
    </li>
  );
}
