using System;
using System.Collections.Generic;
using System.Linq;
using PracticeApi.Models;

namespace PracticeApi.Repositories
{
    public class OTPRepository : RepositoryBase<OTP>, IOTPRepository
    {
        public OTPRepository(PracticalContext repositoryContext) : base(repositoryContext)
        {
        }

    }
}
