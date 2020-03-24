using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.Where(q => q.EmployeeId == employeeid 
                                        && q.LeaveTypeId == leavetypeid 
                                        && q.Period == period)
                .Any();
        }

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            var LeaveAllocations = await _db.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .ToListAsync();
            return LeaveAllocations;
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            var LeaveAllocation = await _db.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == id);
            return LeaveAllocation;
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.Where(q => q.EmployeeId == employeeid && q.Period == period)
                    .ToList();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationsByEmployeeAndType(string employeeid, int leavetypeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.FirstOrDefault(q => q.EmployeeId == employeeid 
                                                    && q.Period == period 
                                                    && q.LeaveTypeId == leavetypeid);
        }

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveAllocations.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}
