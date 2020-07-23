using System;

namespace Domain.Entities.Model
{
    public class FrontUser : ApplicationUser
    {
        #region Properties

        public DateTime? Birthdate { get; set; }

        #endregion

        #region Constructor

        public FrontUser()
        {

        }

        #endregion

        #region Public Methods

        #endregion
    }
}