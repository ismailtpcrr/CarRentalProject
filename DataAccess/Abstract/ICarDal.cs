using Core.DataAccess;
using Entities.Concrede;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICarDal: IEntityRepositoryBase<Car>
    {
    }
}
