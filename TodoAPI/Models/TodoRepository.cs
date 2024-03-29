﻿using System.Collections.Concurrent;
using TodoAPI.Models;

namespace ToDoAPI.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> _todos =
              new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository() // Constructor
        {
            Add(new TodoItem { Name = "Çocuğu okuldan al" });
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString(); // Tek olan bir ID değeri yaratıyor.

            _todos[item.Key] = item;
        }

        public TodoItem Find(string key)
        {
            TodoItem item;

            _todos.TryGetValue(key, out item);

            return item;
        }


        public TodoItem Remove(string key)
        {
            TodoItem item;
            _todos.TryGetValue(key, out item);
            _todos.TryRemove(key, out item);
            return item;
        }

        public void Update(TodoItem item)
        {
            _todos[item.Key] = item;
        }
    }
}