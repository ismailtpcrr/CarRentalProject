using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrede;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrede.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer,CarRentalContext>, ICustomerDal
    {
    }
}
