﻿using PracticeApi.Models;
namespace PracticeApi.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly PracticalContext _repoContext;

        public RepositoryWrapper(PracticalContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        private IHeroRepository? oHero;
        public IHeroRepository Hero
        {
            get
            {
                if (oHero == null)
                {
                    oHero = new HeroRepository(_repoContext);
                }

                return oHero;
            }
        }

        private ICustomerTypeRepository? oCustomerType;
        public ICustomerTypeRepository CustomerType
        {
            get
            {
                if(oCustomerType == null)
                {
                    oCustomerType = new CustomerTypeRepository(_repoContext);
                }
                return oCustomerType;
            }
        }

        private ICustomerRepository? oCustomer;
        public ICustomerRepository Customer
        {
            get
            {
                if(oCustomer == null)
                {
                    oCustomer = new CustomerRepository(_repoContext);
                }
                return oCustomer;
            }
        }


        private IAdminLevelRepository? oAdminLevel;
        public IAdminLevelRepository AdminLevel
        {
            get
            {
                if(oAdminLevel == null)
                {
                    oAdminLevel = new AdminLevelRepository(_repoContext);
                }
                return oAdminLevel;
            }
        }

        private IAdminRepository? oAdmin;
        public IAdminRepository Admin
        {
            get
            {
                if(oAdmin == null)
                {
                    oAdmin = new AdminRepository(_repoContext);
                }
                return oAdmin;
            }
        }

        private IOTPRepository? oOTP;
        public IOTPRepository OTP
        {
            get
            {
                if(oOTP == null)
                {
                    oOTP = new OTPRepository(_repoContext);
                }
                return oOTP;
            }
        }

        private IEventLogRepository? oEventLog;
        public IEventLogRepository EventLog
        {
            get 
            {
                if(oEventLog == null)
                {
                    oEventLog = new EventLogRepository(_repoContext);
                }
                return oEventLog;
            }
        }
    }
}