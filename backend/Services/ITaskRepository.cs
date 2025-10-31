using System;
using System.Collections.Generic;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services
{
    public interface ITaskRepository
    {
        IEnumerable<TaskItem> GetAll();
        TaskItem? Get(Guid id);
        TaskItem Create(TaskItem item);
        bool Update(Guid id, TaskItem item);
        bool Delete(Guid id);
    }
}
