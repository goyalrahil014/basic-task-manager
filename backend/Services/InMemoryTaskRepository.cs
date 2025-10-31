using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TaskManager.Api.Models;

namespace TaskManager.Api.Services
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly ConcurrentDictionary<Guid, TaskItem> _storage = new();

        public InMemoryTaskRepository()
        {
            // Seed with a couple of sample tasks
            var t1 = new TaskItem { Id = Guid.NewGuid(), Description = "Welcome — add a new task!", IsCompleted = false };
            var t2 = new TaskItem { Id = Guid.NewGuid(), Description = "Try completing this task", IsCompleted = false };
            _storage[t1.Id] = t1;
            _storage[t2.Id] = t2;
        }

        public IEnumerable<TaskItem> GetAll() => _storage.Values;

        public TaskItem? Get(Guid id) => _storage.TryGetValue(id, out var item) ? item : null;

        public TaskItem Create(TaskItem item)
        {
            if (item.Id == Guid.Empty) item.Id = Guid.NewGuid();
            _storage[item.Id] = item;
            return item;
        }

        public bool Update(Guid id, TaskItem item)
        {
            if (!_storage.ContainsKey(id)) return false;
            // Ensure id consistency
            item.Id = id;
            _storage[id] = item;
            return true;
        }

        public bool Delete(Guid id) => _storage.TryRemove(id, out _);
    }
}
