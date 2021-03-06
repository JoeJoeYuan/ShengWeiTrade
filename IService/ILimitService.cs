﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;

namespace IService
{
    public interface ILimitService
    {
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<Limit> GetAll();
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        Limit GetById(int id);
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="limit">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        List<Limit> GetByPage(int startIndex, int count, string sortColumn, Limit limit);
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="limit">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        int GetCount(Limit limit);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="limit">需要更新的数据实体</param>
        /// <returns></returns>
        int Update(Limit limit);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        int Delete(params int[] ids);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="limit">需要插入的数据实体</param>
        /// <returns></returns>
        int Insert(Limit limit);
        /// <summary>
        /// 根据UID删除数据
        /// </summary>
        /// <param name="UID">uid</param>
        /// <returns></returns>
        int DeleteUID(int UID);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int Insert(List<Limit> list);
        /// <summary>
        /// 根据用户ID获取该用户权限
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        List<Limit> GetOperate(int UID);
    }
}
