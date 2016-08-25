﻿using Abp.Zero.EntityFramework;
using Cinotam.AbpModuleZero.Authorization.Roles;
using Cinotam.AbpModuleZero.MultiTenancy;
using Cinotam.AbpModuleZero.Users;
using System.Data.Common;

namespace Cinotam.AbpModuleZero.EntityFramework
{
    public class AbpModuleZeroDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public AbpModuleZeroDbContext()
            : base("Default")
        {
            //DbInterception.Add(new FullTextInterceptor());
        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in AbpModuleZeroDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of AbpModuleZeroDbContext since ABP automatically handles it.
         */
        public AbpModuleZeroDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public AbpModuleZeroDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}