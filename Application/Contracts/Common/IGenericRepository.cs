using MediatR;

namespace Application.Contracts.Common;

public interface IGenericRepository<T> where T : class
{
    //get all
    public Task<IReadOnlyList<T>> GetAllAsync();

    //get by id
    public Task<T> GetByIdAsync(int Id);

    //create new
    public Task<T> CreateAsync(T entity);


    //update 
    public Task<Unit> UpdateAsync(int id, T entity);


    //delete 
    public Task<Unit> DeleteAsync(int id);
}