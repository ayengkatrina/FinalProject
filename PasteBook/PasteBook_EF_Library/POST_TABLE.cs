//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasteBook_EF_Library
{
    using System;
    using System.Collections.Generic;
    
    public partial class POST_TABLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POST_TABLE()
        {
            this.COMMENTS_TABLE = new HashSet<COMMENTS_TABLE>();
            this.LIKES_TABLE = new HashSet<LIKES_TABLE>();
            this.NOTIFICATION_TABLE = new HashSet<NOTIFICATION_TABLE>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public string CONTENT { get; set; }
        public int PROFILE_ID { get; set; }
        public int POSTER_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTS_TABLE> COMMENTS_TABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIKES_TABLE> LIKES_TABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION_TABLE> NOTIFICATION_TABLE { get; set; }
        public virtual USER_TABLE USER_TABLE { get; set; }
        public virtual USER_TABLE USER_TABLE1 { get; set; }
    }
}
