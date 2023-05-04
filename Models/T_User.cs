//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaziMarket1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_User()
        {
            this.T_Comment = new HashSet<T_Comment>();
            this.T_Question1 = new HashSet<T_Question>();
            this.T_Product = new HashSet<T_Product>();
        }
    
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public System.DateTime UserRegisterDate { get; set; }
        public bool UserIsActive { get; set; }
        public string ProfileImage { get; set; }
        public string UserAddress { get; set; }
        public byte UserRoleId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    
        public virtual T_Cart T_Cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Comment> T_Comment { get; set; }
        public virtual T_Role T_Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Question> T_Question1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Product> T_Product { get; set; }
    }
}