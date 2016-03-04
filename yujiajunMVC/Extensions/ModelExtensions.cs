using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using yujiajunMVC.Areas.Back.Models;
using AutoMapper;

namespace yujiajunMVC
{
    public static class ModelExtensions
    {
        public static NavigationModel ToNavModel(this Navigation nav)
        {
           return AutoMapper.Mapper.Map<Navigation, NavigationModel>(nav);
        }
        public static Navigation ToEntity(this NavigationModel model)
        {
            return AutoMapper.Mapper.Map<NavigationModel, Navigation>(model);
        }
        public static UserModel ToModel(this Users user)
        {
            return AutoMapper.Mapper.Map<Users, UserModel>(user);
        }
        public static Users ToEntity(this UserModel model)
        {
            return AutoMapper.Mapper.Map<UserModel, Users>(model);
        }
        public static FunctionModel ToModel(this Functions function)
        {
            return AutoMapper.Mapper.Map<Functions, FunctionModel>(function);
        }
        public static Functions ToEntity(this FunctionModel model)
        {
            return AutoMapper.Mapper.Map<FunctionModel, Functions>(model);
        }
    }
}