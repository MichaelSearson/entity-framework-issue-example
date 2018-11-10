using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkIssueExample.Entities
{
    /// <summary>
    /// Minimal set of properties necessary to demonstrate issue.
    /// </summary>
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }

        #region Audit

        public Guid? AddedByAccountId { get; set; }

        public DateTime AddedOnUtc { get; set; }

        public Guid? ModifiedByAccountId { get; set; }

        public DateTime ModifiedOnUtc { get; set; }

        #endregion Audit

        #region Navigation Properties

        [ForeignKey(nameof(AddedByAccountId))]
        public virtual Account AddedByAccount { get; set; }

        [ForeignKey(nameof(ModifiedByAccountId))]
        public virtual Account ModifiedByAccount { get; set; }

        #endregion Navigation Properties
    }
}