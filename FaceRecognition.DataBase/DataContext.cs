namespace FaceRecognition.DataBase
{
    using FaceRecognition.DataBase.Schema;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Z.EntityFramework.Plus;

    public class DataContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Imager> Imagers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TypeOfInput> TypeOfInputs { get; set; }
        public virtual DbSet<TypeOfTraining> TypeOfTrainings { get; set; }
        public DataContext()
            : base(@"data source=DESKTOP-OVLNOKF;initial catalog=DATN_FaceRecognition;User Id=sa;Password=123456")
            //: base(@"data source=minhhoang.database.windows.net;initial catalog=DATN_FaceRecognition;User Id=MinhHoang;Password=_NguyenMinh123")
        {
        }
        public override int SaveChanges()
        {
            try
            {
                if (ChangeTracker.HasChanges())
                {
                    foreach (var entry
                        in ChangeTracker.Entries())
                    {
                        try
                        {
                            var root = (Schema.Table)entry.Entity;
                            var now = DateTime.Now;
                            switch (entry.State)
                            {
                                case EntityState.Added:
                                    {
                                        root.Created_at = now;
                                        root.Created_by = TaoDataBase.GetIdAccount();
                                        root.Updated_at = null;
                                        root.Updated_by = null;
                                        root.DelFlag = false;
                                        break;
                                    }
                                case EntityState.Modified:
                                    {
                                        root.Updated_at = now;
                                        root.Updated_by = TaoDataBase.GetIdAccount();
                                        break;
                                    }
                            }
                        }
                        catch
                        {
                        }
                    }

                    var audit = new Audit();
                    audit.PreSaveChanges(this);
                    var rowAffecteds = base.SaveChanges();
                    audit.PostSaveChanges();

                    if (audit.Configuration.AutoSavePreAction != null)
                    {
                        audit.Configuration.AutoSavePreAction(this, audit);
                    }

                    return base.SaveChanges();
                }

                return 0;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class TaoDataBase : CreateDatabaseIfNotExists<DataContext>
        {




            /// <summary>
            /// Get IdAccount đang login
            /// Author       :   HoangNM - 25/02/2019 - create
            /// </summary>
            /// <returns>
            /// IdAccount nếu tồn tại, trả về 0 nếu không tồn tại
            /// </returns>
            public static int GetIdAccount()
            {
                try
                {

                    return 1;
                }
                catch
                {
                    return 0;
                }
            }

        }

    }
}