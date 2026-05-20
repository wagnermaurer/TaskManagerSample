function TaskList({ tasks, sort, onSort, onToggle, onDelete }) {
  return (
    <div className="task-list-wrapper">
      <TaskListHeader sort={sort} onSort={onSort} />
      {tasks.length === 0 ? (
        <p className="empty">No tasks yet. Add one above.</p>
      ) : (
        <ul className="task-list">
          {tasks.map((task) => (
            <TaskItem
              key={task.id}
              task={task}
              onToggle={onToggle}
              onDelete={onDelete}
            />
          ))}
        </ul>
      )}
    </div>
  );
}
