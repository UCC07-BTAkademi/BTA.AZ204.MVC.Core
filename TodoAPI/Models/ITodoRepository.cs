namespace TodoAPI.Models
{
    public interface ITodoRepository
    {

        void Add(TodoItem item); // Todo ekleme
        IEnumerable<TodoItem> GetAll(); // Todoları listeleme
        TodoItem Find(string key); // Find
        TodoItem Remove(string key); // remove
        void Update(TodoItem item); // update

    }
}
