use master
go
if exists(select * from sysdatabases where name='yujiajunMvc')
drop database yujiajunMvc
go
create database yujiajunMvc
on
(
	name='yujiajunMvc',
	filename='D:\yujiajunMvc_data.mdf'
)
log on
(
	name='yujiajunMvc_log',
	filename='D:\yujiajunMvc_log.ldf'
)
go
use yujiajunMvc
go
if OBJECT_ID('Navigation') is not null
drop table Navigation
go
if OBJECT_ID('News') is not null
drop table News
go
if OBJECT_ID('Messages') is not null
drop table Messages
go
if OBJECT_ID('Users') is not null
drop table Users
go
if OBJECT_ID('Functions') is not null
drop table Functions
go
if OBJECT_ID('Limit') is not null
drop table Limit
go
if OBJECT_ID('Links') is not null
drop table Links
go
--导航表
create table Navigation
(
	ID int identity(1,1) primary key not null,
	ParentID int,            --父级ID
	NName nvarchar(100),	 --导航名称
	IsLock int,				 --是否显示(0:否 1:是)
	IsImg int default(0),	 --是否保存图片地址(主要用于图片新闻或产品)0:否 1:是
	Sort int default(0),	 --排序
	NPath nvarchar(200),	 --导航路径
)
go
--新闻表
create table News
(
	ID int identity(1,1)primary key not null,
	NID int references Navigation(ID) on delete cascade,--导航ID
	Title nvarchar(200),								--标题
	Author nvarchar(50),							--发布人
	CreateTime datetime default(getdate()),				--发布时间
	Source nvarchar(200),								--文章来源
	Click int default(0),								--点击数
	DownLoadNum int default(0),							--下载数
	CommentNum int,										--评论数
	IsFile int default(0),								--是否有附件(0:否 1:视频 2:其它 在config中配置)
	FilePath nvarchar(100),								--附件地址
	ImagePath nvarchar(100),							--图片地址(防止单独显示产品图片)
	FileDescription nvarchar(600),						--附件描述
	NewsContent text,									--内容
	IsHot int default(0),								--是否推荐(0:否 1:是)
	IsTop int default(0),								--是否置顶(0:否 1:是)
)
go
--留言表
create table Messages
(
	ID int identity(1,1) primary key not null,
	IP nvarchar(20),						--留言IP
	CreateTime datetime default(getdate()),	--留言时间
	IsAudit int,							--是否审核(0:否 1:是)
	CreateName nvarchar(200),				--留言姓名
	MessageContent nvarchar(1000),			--留言内容
)
go
--用户表
create table Users
(
	ID int identity(1,1) primary key not null,
	UserName nvarchar(50),					--用户名
	UserPassword nvarchar(50),				--密码
	IsAdmin int,							--是否管理员(0:否 1:是)
	IsLock int,								--是否启用(0:否 1:是)
)
--功能表
create table Functions
(
	ID int identity(1,1) primary key not null,
	ParentID int,					--父级ID (Note:如功能较少 则全为默认值0,连接路径不能为空)
	FName nvarchar(50),				--功能名称
	FPath nvarchar(200),			--连接路径
	IsLock int,						--是否启用
)
go
--权限表
create table Limit
(
	ID int identity(1,1) primary key not null,
	FID int references Functions(ID) on delete cascade,	--功能ID
	UID int references Users(ID) on delete cascade,		--用户ID
	Operate nvarchar(22),								--拥有权限
)
go
--友情连接
create table Links
(
	ID int identity(1,1) primary key not null,
	LName nvarchar(50),									--链接名称
	LPath nvarchar(500),								--链接路径
)
go
if OBJECT_ID('proc_GetByPage') is not null
drop proc proc_GetByPage
go
create proc [dbo].proc_GetByPage
@count int, --要取的条数
@startIndex int,--从第几条开始取
@order nvarchar(50),--排序
@tableName nvarchar(50),--表名
@pkName nvarchar(50),--列名
@where nvarchar(max) --查询条件(如无查询条件传入"")
as
declare @sql nvarchar(max)
set @sql='select top '+convert(nvarchar(10),@count)+' * from '+@tableName+' where '+@pkName+' not in (select top '+convert(nvarchar(10),@startIndex)+' '+@pkName+' from '+@tableName+' where 1=1'+@where+' order by '+@order+') '+@where+' order by '+@order
exec (@sql)

--前台新闻
--go
--if OBJECT_ID('GetFrontNews') is not null
--drop proc GetFrontNews
--go
--create proc GetFrontNews
--as
--with t1 as(
--select top 10 ID,NID,Title,ImagePath from News where ImagePath!='0' ORDER BY ID desc --产品中心
--),
--t2 as(
--select top 10 ID,NID,Title,ImagePath from News where NID=5 ORDER BY ID desc --网站公告
--),
--t3 as(
--	select top 8 ID,NID,Title,ImagePath from News where NID= 4  ORDER BY ID desc --技术中心
--),
--t4 as(
--	select top 8 ID,NID,Title,ImagePath from News where NID=3 ORDER BY ID desc --行业咨询
--),
--t5 as(
--	select top 8 ID,NID,Title,ImagePath from News where NID=2 ORDER BY ID desc --企业新闻
--)
--select ID,NID,Title,ImagePath from t1 union all 
--select ID,NID,Title,ImagePath from t2 union all
--select ID,NID,Title,ImagePath from t3 union all
--select ID,NID,Title,ImagePath from t4 union all
--select ID,NID,Title,ImagePath from t5
go
--前台新闻
if OBJECT_ID('GetFrontNews') is not null
drop proc GetFrontNews
go
create proc GetFrontNews
as
select * from (select top 10 ID,NID,Title,ImagePath,CreateTime from News where ImagePath!='0' ORDER BY ID desc) AS TABLE1--产品中心
union all
select * from (select top 10 ID,NID,Title,ImagePath,CreateTime from News where NID=5 ORDER BY ID desc) AS TABLE2--网站公告
union all
select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from News where NID= 4  ORDER BY ID desc) AS TABLE3--技术中心
union all
select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from News where NID=3 ORDER BY ID desc) AS TABLE4--行业咨询
union all
select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from News where NID=2 ORDER BY ID desc) AS TABLE5--企业新闻
go

---修改点击数
if OBJECT_ID('SetClick') is not null
drop proc SetClick
go
create proc SetClick
@ID int
as
update News set Click=Click+1 where ID=@ID
go

---修改下载数
if OBJECT_ID('SetDownLoadNum') is not null
drop proc SetDownLoadNum
go
create proc SetDownLoadNum
@ID int
as
update News set DownLoadNum=DownLoadNum+1 where ID=@ID

/**********************
	
是否有附件(附件类型：0没有附件
					 1其它 可动态添加 config配置)

Note: 表中涉及查询字段不能有null值  用数字代替
*****************/

insert into Navigation(ParentID,NName,IsLock,IsImg,Sort,NPath)
select 0,'新闻中心',1,0,0,'NewsList'
union all
select 1,'企业新闻',1,0,0,'NewsList'
union all
select 1,'行业咨询',1,0,0,'NewsList'
union all
select 1,'技术中心',1,0,0,'NewsList'
union all
select 1,'网站公告',1,0,0,'NewsList'
union all
select 0,'产品中心',1,1,0,'ProductsList'
union all
select 6,'风景画',1,1,0,'ProductsList'
union all
select 0,'留言',1,0,0,'Message'
go
insert into Functions
select 0,'网站管理','',1
union all
select 1,'功能管理','/Back/Items/FunctionList',1
union all
select 1,'导航管理','/Back/Items/NavigationList',1
union all
select 1,'用户管理','/Back/Items/UserList',1
union all
select 1,'友情连接管理','/Back/Items/LinkList',1
union all
select 1,'关于我们','/Back/Items/AboutUs',1
union all
select 1,'首页图片滚动','/Back/Items/RollPicture',1
union all
select 1,'留言管理','/Back/Message/Index',1
union all
select 1,'新闻管理','/Back/News/Index',1
union all
select 0,'其它','',1
union all
select 10,'修改文件','/Back/CompanyFileManage/Index',1

