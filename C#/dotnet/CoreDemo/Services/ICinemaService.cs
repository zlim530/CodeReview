using CoreDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreDemo.Services {
    public interface ICinemaService {
        Task<IEnumerable<Cinema>> GetAllAsync();
        Task<Cinema> GetByIdAsync(int id);
        Task AddAsync(Cinema model);
    }
}
