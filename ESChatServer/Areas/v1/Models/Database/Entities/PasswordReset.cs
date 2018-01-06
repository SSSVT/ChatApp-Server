using System;

namespace ESChatServer.Areas.v1.Models.Database.Entities
{
    public class PasswordReset
    {
        public Guid Id { get; set; }
        public long UserId { get; set; }
        public DateTime UtcIssued { get; set; }
        public DateTime UtcExpiration { get; set; }
        public bool Used { get; set; }

        #region Virtual
        public virtual User User { get; set; }
        #endregion
    }
}
