﻿using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab
{
    public class TrackOrderDataAccess : BaseDataAccess<TrackOrder>, ITrackOrderDataAccess
    {
        public TrackOrderDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<TrackOrder>> GetList()
        {
            return await base.GetList("Tab_GetTrackOrders");
        }
    }
}


