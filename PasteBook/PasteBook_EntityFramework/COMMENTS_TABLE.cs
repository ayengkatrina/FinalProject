//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasteBook_EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class COMMENTS_TABLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMMENTS_TABLE()
        {
            this.NOTIFICATION_TABLE = new HashSet<NOTIFICATION_TABLE>();
        }
    
        public int ID { get; set; }
        public int POST_ID { get; set; }
        public int POSTER_ID { get; set; }
        public string CONTENT { get; set; }
        public Nullable<System.DateTime> DATE_CREATED { get; set; }
    
        public virtual POST_TABLE POST_TABLE { get; set; }
        public virtual USER_TABLE USER_TABLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION_TABLE> NOTIFICATION_TABLE { get; set; }
    }
}
