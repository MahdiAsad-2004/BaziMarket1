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
    
    public partial class T_Cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Cart()
        {
            this.T_ProductCart = new HashSet<T_ProductCart>();
        }
    
        public int CartId { get; set; }
        public int CartCost { get; set; }
    
        public virtual T_User T_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ProductCart> T_ProductCart { get; set; }
    }
}
