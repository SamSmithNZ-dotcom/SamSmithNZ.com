// <copyright file="PlayerDATest.cs">Copyright ©  2017</copyright>
using System;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data.Tests
{
    /// <summary>This class contains parameterized unit tests for PlayerDA</summary>
    [PexClass(typeof(PlayerDA))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class PlayerDATest
    {
        /// <summary>Test stub for GetDataAsync(String)</summary>
        [PexMethod]
        public Task<Player> GetDataAsyncTest([PexAssumeUnderTest]PlayerDA target, string steamID)
        {
            Task<Player> result = target.GetDataAsync(steamID);
            return result;
            // TODO: add assertions to method PlayerDATest.GetDataAsyncTest(PlayerDA, String)
        }
    }
}
