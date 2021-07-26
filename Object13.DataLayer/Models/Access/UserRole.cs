using Object13.DataLayer.Models.Account;
using Object13.DataLayer.Models.Main;

namespace Object13.DataLayer.Models.Access
{
    public class UserRole:BaseModel
    {
        #region Propersties
        public long UserID { get; set; }
        public long RoleID { get; set; }
        #endregion

        #region Relation
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
