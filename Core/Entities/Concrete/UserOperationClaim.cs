using Cor.Entities;

namespace Core.Entities.Concrete
{

    
        public class UserOperationClaim:IEntity
        {
            public int Id { get; set; }
            public int userId { get; set; }
            public int OperationClaimId { get; set; }
        }

    
}
