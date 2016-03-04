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
--������
create table Navigation
(
	ID int identity(1,1) primary key not null,
	ParentID int,            --����ID
	NName nvarchar(100),	 --��������
	IsLock int,				 --�Ƿ���ʾ(0:�� 1:��)
	IsImg int default(0),	 --�Ƿ񱣴�ͼƬ��ַ(��Ҫ����ͼƬ���Ż��Ʒ)0:�� 1:��
	Sort int default(0),	 --����
	NPath nvarchar(200),	 --����·��
)
go
--���ű�
create table News
(
	ID int identity(1,1)primary key not null,
	NID int references Navigation(ID) on delete cascade,--����ID
	Title nvarchar(200),								--����
	Author nvarchar(50),							--������
	CreateTime datetime default(getdate()),				--����ʱ��
	Source nvarchar(200),								--������Դ
	Click int default(0),								--�����
	DownLoadNum int default(0),							--������
	CommentNum int,										--������
	IsFile int default(0),								--�Ƿ��и���(0:�� 1:��Ƶ 2:���� ��config������)
	FilePath nvarchar(100),								--������ַ
	ImagePath nvarchar(100),							--ͼƬ��ַ(��ֹ������ʾ��ƷͼƬ)
	FileDescription nvarchar(600),						--��������
	NewsContent text,									--����
	IsHot int default(0),								--�Ƿ��Ƽ�(0:�� 1:��)
	IsTop int default(0),								--�Ƿ��ö�(0:�� 1:��)
)
go
--���Ա�
create table Messages
(
	ID int identity(1,1) primary key not null,
	IP nvarchar(20),						--����IP
	CreateTime datetime default(getdate()),	--����ʱ��
	IsAudit int,							--�Ƿ����(0:�� 1:��)
	CreateName nvarchar(200),				--��������
	MessageContent nvarchar(1000),			--��������
)
go
--�û���
create table Users
(
	ID int identity(1,1) primary key not null,
	UserName nvarchar(50),					--�û���
	UserPassword nvarchar(50),				--����
	IsAdmin int,							--�Ƿ����Ա(0:�� 1:��)
	IsLock int,								--�Ƿ�����(0:�� 1:��)
)
--���ܱ�
create table Functions
(
	ID int identity(1,1) primary key not null,
	ParentID int,					--����ID (Note:�繦�ܽ��� ��ȫΪĬ��ֵ0,����·������Ϊ��)
	FName nvarchar(50),				--��������
	FPath nvarchar(200),			--����·��
	IsLock int,						--�Ƿ�����
)
go
--Ȩ�ޱ�
create table Limit
(
	ID int identity(1,1) primary key not null,
	FID int references Functions(ID) on delete cascade,	--����ID
	UID int references Users(ID) on delete cascade,		--�û�ID
	Operate nvarchar(22),								--ӵ��Ȩ��
)
go
--��������
create table Links
(
	ID int identity(1,1) primary key not null,
	LName nvarchar(50),									--��������
	LPath nvarchar(500),								--����·��
)
go
if OBJECT_ID('proc_GetByPage') is not null
drop proc proc_GetByPage
go
create proc [dbo].proc_GetByPage
@count int, --Ҫȡ������
@startIndex int,--�ӵڼ�����ʼȡ
@order nvarchar(50),--����
@tableName nvarchar(50),--����
@pkName nvarchar(50),--����
@where nvarchar(max) --��ѯ����(���޲�ѯ��������"")
as
declare @sql nvarchar(max)
set @sql='select top '+convert(nvarchar(10),@count)+' * from '+@tableName+' where '+@pkName+' not in (select top '+convert(nvarchar(10),@startIndex)+' '+@pkName+' from '+@tableName+' where 1=1'+@where+' order by '+@order+') '+@where+' order by '+@order
exec (@sql)

--ǰ̨����
--go
--if OBJECT_ID('GetFrontNews') is not null
--drop proc GetFrontNews
--go
--create proc GetFrontNews
--as
--with t1 as(
--select top 10 ID,NID,Title,ImagePath from News where ImagePath!='0' ORDER BY ID desc --��Ʒ����
--),
--t2 as(
--select top 10 ID,NID,Title,ImagePath from News where NID=5 ORDER BY ID desc --��վ����
--),
--t3 as(
--	select top 8 ID,NID,Title,ImagePath from News where NID= 4  ORDER BY ID desc --��������
--),
--t4 as(
--	select top 8 ID,NID,Title,ImagePath from News where NID=3 ORDER BY ID desc --��ҵ��ѯ
--),
--t5 as(
--	select top 8 ID,NID,Title,ImagePath from News where NID=2 ORDER BY ID desc --��ҵ����
--)
--select ID,NID,Title,ImagePath from t1 union all 
--select ID,NID,Title,ImagePath from t2 union all
--select ID,NID,Title,ImagePath from t3 union all
--select ID,NID,Title,ImagePath from t4 union all
--select ID,NID,Title,ImagePath from t5
go
--ǰ̨����
if OBJECT_ID('GetFrontNews') is not null
drop proc GetFrontNews
go
create proc GetFrontNews
as
select * from (select top 10 ID,NID,Title,ImagePath,CreateTime from News where ImagePath!='0' ORDER BY ID desc) AS TABLE1--��Ʒ����
union all
select * from (select top 10 ID,NID,Title,ImagePath,CreateTime from News where NID=5 ORDER BY ID desc) AS TABLE2--��վ����
union all
select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from News where NID= 4  ORDER BY ID desc) AS TABLE3--��������
union all
select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from News where NID=3 ORDER BY ID desc) AS TABLE4--��ҵ��ѯ
union all
select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from News where NID=2 ORDER BY ID desc) AS TABLE5--��ҵ����
go

---�޸ĵ����
if OBJECT_ID('SetClick') is not null
drop proc SetClick
go
create proc SetClick
@ID int
as
update News set Click=Click+1 where ID=@ID
go

---�޸�������
if OBJECT_ID('SetDownLoadNum') is not null
drop proc SetDownLoadNum
go
create proc SetDownLoadNum
@ID int
as
update News set DownLoadNum=DownLoadNum+1 where ID=@ID

/**********************
	
�Ƿ��и���(�������ͣ�0û�и���
					 1���� �ɶ�̬��� config����)

Note: �����漰��ѯ�ֶβ�����nullֵ  �����ִ���
*****************/

insert into Navigation(ParentID,NName,IsLock,IsImg,Sort,NPath)
select 0,'��������',1,0,0,'NewsList'
union all
select 1,'��ҵ����',1,0,0,'NewsList'
union all
select 1,'��ҵ��ѯ',1,0,0,'NewsList'
union all
select 1,'��������',1,0,0,'NewsList'
union all
select 1,'��վ����',1,0,0,'NewsList'
union all
select 0,'��Ʒ����',1,1,0,'ProductsList'
union all
select 6,'�羰��',1,1,0,'ProductsList'
union all
select 0,'����',1,0,0,'Message'
go
insert into Functions
select 0,'��վ����','',1
union all
select 1,'���ܹ���','/Back/Items/FunctionList',1
union all
select 1,'��������','/Back/Items/NavigationList',1
union all
select 1,'�û�����','/Back/Items/UserList',1
union all
select 1,'�������ӹ���','/Back/Items/LinkList',1
union all
select 1,'��������','/Back/Items/AboutUs',1
union all
select 1,'��ҳͼƬ����','/Back/Items/RollPicture',1
union all
select 1,'���Թ���','/Back/Message/Index',1
union all
select 1,'���Ź���','/Back/News/Index',1
union all
select 0,'����','',1
union all
select 10,'�޸��ļ�','/Back/CompanyFileManage/Index',1

