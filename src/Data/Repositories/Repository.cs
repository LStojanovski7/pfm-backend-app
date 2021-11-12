namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        
    }
}