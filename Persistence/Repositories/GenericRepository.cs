using Application.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDBContext _dbContext;

        protected GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var item = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<Unit> DeleteAsync(int Id)
        {
            var item = await GetByIdAsync(Id);
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<bool> Exists(int id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);
            
            return item != null;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            var item = await _dbContext.Set<T>().FindAsync(Id);
            return item;
        }

        public async Task<Unit> UpdateAsync(int Id, T entity)
        {
            var item = await GetByIdAsync(Id);
            _dbContext.Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Unit.Value;  
        }
    }
}
