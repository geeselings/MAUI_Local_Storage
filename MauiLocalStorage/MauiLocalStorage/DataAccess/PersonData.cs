using SQLite;
using MauiLocalStorage.Models;

namespace MauiLocalStorage.DataAccess
{
    public class PersonData
    {
        SQLiteAsyncConnection database;

        async Task InitializeDatabase()
        {
            if (database is not null)
            {
                return;
            }
            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            await database.CreateTableAsync<Person>();
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            await InitializeDatabase();
            return await database.Table<Person>().ToListAsync();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            await InitializeDatabase();
            return await database.Table<Person>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task SavePersonAsync(Person person)
        {
            await InitializeDatabase();
            if (person.ID != 0)
            {
                await database.UpdateAsync(person);
            }
            else
            {
                await database.InsertAsync(person);
            }
        }

        public async Task DeletePersonAsync(Person person)
        {
            await InitializeDatabase();
            await database.DeleteAsync(person);
        }
    }
}
