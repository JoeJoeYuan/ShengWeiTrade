using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Models;
using yujiajunMVC.Areas.Back.Models;

namespace yujiajunMVC
{
    public class AutoMapperModel
    {
        public static void Mapper()
        {
            Result<Navigation, NavigationModel>();

            Result<Users, UserModel>();

            Result<Functions, FunctionModel>();

        }
        private static void Result<T, V>()
        {
            AutoMapper.Mapper.CreateMap<T, V>();
            AutoMapper.Mapper.CreateMap<V, T>();
        }
    }
}