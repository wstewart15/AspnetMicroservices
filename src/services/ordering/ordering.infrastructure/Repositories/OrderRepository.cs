using Microsoft.EntityFrameworkCore;
using ordering.application.Contracts.Persistence;
using ordering.domain.Entities;
using ordering.infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordering.infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                .Where(o => o.UserName == userName)
                                .ToListAsync();
            return orderList;
        }
    }
}
