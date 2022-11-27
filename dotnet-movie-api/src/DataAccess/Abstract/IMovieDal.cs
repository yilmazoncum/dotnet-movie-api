﻿using dotnet_movie_api.src.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace dotnet_movie_api.src.DataAccess
{
    public interface IGenericDal<T>
    {
        List<T> GetList(T t);
        T Get (int id);
        void Add(T t);
        void Update(T t);
        void Delete(T t);
    }
}
