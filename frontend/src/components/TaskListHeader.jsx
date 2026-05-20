function TaskListHeader({ sort, onSort }) {
  function arrow(field) {
    if (sort.field !== field) return "";
    return sort.direction === "asc" ? " ↑" : " ↓";
  }

  return (
    <div className="task-list-header">
      <span className="task-checkbox-spacer" aria-hidden="true" />
      <button
        type="button"
        className={`task-sort-button ${sort.field === "title" ? "active" : ""}`}
        onClick={() => onSort("title")}
      >
        Task{arrow("title")}
      </button>
      <button
        type="button"
        className={`task-sort-button ${sort.field === "createdAt" ? "active" : ""}`}
        onClick={() => onSort("createdAt")}
      >
        Created{arrow("createdAt")}
      </button>
      <span className="task-delete-spacer" aria-hidden="true" />
    </div>
  );
}
