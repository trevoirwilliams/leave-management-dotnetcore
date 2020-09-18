using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private  IGenericRepository<LeaveType> _leaveTypes;
        private  IGenericRepository<LeaveRequest> _leaveRequests;
        private  IGenericRepository<LeaveAllocation> _leaveAllocations;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<LeaveType> LeaveTypes
            => _leaveTypes ??= new GenericRepository<LeaveType>(_context);
            
        public IGenericRepository<LeaveRequest> LeaveRequests
              => _leaveRequests ??= new GenericRepository<LeaveRequest>(_context);
        public IGenericRepository<LeaveAllocation> LeaveAllocations
              => _leaveAllocations ??= new GenericRepository<LeaveAllocation>(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if(dispose)
            {
                _context.Dispose();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
