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
    
    public partial class T_ProductCart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_ProductCart()
        {
            this.T_Cart = new HashSet<T_Cart>();
        }
    
        public int ProductCartId { get; set; }
        public Nullable<int> ProductCartCount { get; set; }
        public Nullable<int> ProductId { get; set; }
    
        public virtual T_Product T_Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Cart> T_Cart { get; set; }
    }
}