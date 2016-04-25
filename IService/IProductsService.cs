﻿using System.Collections.Generic;
using Models;

namespace IService
{
    public interface IProductsService
    {
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<Products> GetAll();
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        Products GetById(int id);
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="new">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        List<Products> GetByPage(int startIndex, int count, string sortColumn, Products Products);
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="new">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        int GetCount(Products products);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="new">需要更新的数据实体</param>
        /// <returns></returns>
        int Update(Products products);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        int Delete(params int[] ids);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="new">需要插入的数据实体</param>
        /// <returns></returns>
        int Insert(Products products);
        ///// <summary>
        ///// 修改点击数
        ///// </summary>
        ///// <param name="ID"></param>
        //void UpdateClick(int ID);
        ///// <summary>
        ///// 修改下载数
        ///// </summary>
        ///// <param name="ID"></param>
        //void UpdateDownLoad(int ID);
        ///// <summary>
        ///// 前台产品查询
        ///// </summary>
        ///// <returns></returns>
        //List<News> GetByPage();
        /// <summary>
        /// 热点产品
        /// </summary>
        /// <returns></returns>
        List<Products> GetHot();
    }
}