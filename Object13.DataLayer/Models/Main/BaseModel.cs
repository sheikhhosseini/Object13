using System;
using System.ComponentModel.DataAnnotations;


namespace Object13.DataLayer.Models.Main
{
    public class BaseModel
    {
        #region Properties
        [Key]
        public long Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        #endregion
    }
}
