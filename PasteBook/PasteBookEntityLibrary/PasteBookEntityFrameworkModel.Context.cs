﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasteBookEntityLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PasteBookDBEntities : DbContext
    {
        public PasteBookDBEntities()
            : base("name=PasteBookDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<COMMENTS_TABLE> COMMENTS_TABLE { get; set; }
        public virtual DbSet<FRIENDS_TABLE> FRIENDS_TABLE { get; set; }
        public virtual DbSet<LIKES_TABLE> LIKES_TABLE { get; set; }
        public virtual DbSet<NOTIFICATION_TABLE> NOTIFICATION_TABLE { get; set; }
        public virtual DbSet<POST_TABLE> POST_TABLE { get; set; }
        public virtual DbSet<REF_COUNTRY> REF_COUNTRY { get; set; }
        public virtual DbSet<USER_TABLE> USER_TABLE { get; set; }
    
        public virtual ObjectResult<POST_USER_JOIN_Result> POST_USER_JOIN()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<POST_USER_JOIN_Result>("POST_USER_JOIN");
        }
    
        public virtual ObjectResult<FRIEND_POST_USER_JOIN_Result> FRIEND_POST_USER_JOIN()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FRIEND_POST_USER_JOIN_Result>("FRIEND_POST_USER_JOIN");
        }
    
        public virtual ObjectResult<FRIEND_POST_USER_JOIN1_Result> FRIEND_POST_USER_JOIN1(Nullable<int> profileID)
        {
            var profileIDParameter = profileID.HasValue ?
                new ObjectParameter("profileID", profileID) :
                new ObjectParameter("profileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FRIEND_POST_USER_JOIN1_Result>("FRIEND_POST_USER_JOIN1", profileIDParameter);
        }
    
        public virtual ObjectResult<FRIEND_POST_USER_JOIN2_Result> FRIEND_POST_USER_JOIN2(Nullable<int> profileID)
        {
            var profileIDParameter = profileID.HasValue ?
                new ObjectParameter("profileID", profileID) :
                new ObjectParameter("profileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FRIEND_POST_USER_JOIN2_Result>("FRIEND_POST_USER_JOIN2", profileIDParameter);
        }
    
        public virtual ObjectResult<FRIEND_POST_USER_JOIN3_Result> FRIEND_POST_USER_JOIN3(Nullable<int> profileID)
        {
            var profileIDParameter = profileID.HasValue ?
                new ObjectParameter("profileID", profileID) :
                new ObjectParameter("profileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FRIEND_POST_USER_JOIN3_Result>("FRIEND_POST_USER_JOIN3", profileIDParameter);
        }
    
        public virtual ObjectResult<NEWSFEEDPOST_Result> NEWSFEEDPOST(Nullable<int> profileID)
        {
            var profileIDParameter = profileID.HasValue ?
                new ObjectParameter("profileID", profileID) :
                new ObjectParameter("profileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NEWSFEEDPOST_Result>("NEWSFEEDPOST", profileIDParameter);
        }
    }
}
