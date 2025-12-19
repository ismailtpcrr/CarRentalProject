using Core.DataAccess;
using Core.Entities.Concrede;

namespace DataAccess.Abstract;


public interface IUserDal:IEntityRepositoryBase<User>
{
    List<OperationClaim> GetClaims(User user);
}