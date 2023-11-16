﻿using Dapper;
using SamSmithNZ.Service.Models.ITunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.ITunes.Interfaces
{
    public interface IPlaylistDataAccess
    {
        Task<List<Playlist>> GetList();
        Task<Playlist> GetItem(int playlistCode);
        //Task<int> SaveItem(DateTime dteDate);
        //Task<bool> DeleteItem(int playlistCode);

    }
}