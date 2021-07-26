using Object13.DataLayer.Models.Account;

namespace Object13.DataLayer.Models.Access
{
    public class UserRole
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
