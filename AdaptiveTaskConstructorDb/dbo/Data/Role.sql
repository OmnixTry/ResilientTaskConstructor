/*
Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся инструкции SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE [dbo].[AspNetRoles] AS Target
USING (
    SELECT * 
    FROM (VALUES 
        ('16c9c0ce-04ca-43a5-9635-f8d46f80a323', '5a033aaa-6293-413e-9c8e-554988105c2e', 'Teacher', 'TEACHER'),
        ('83cb0625-55e9-4bac-b5c1-dd28d7283cb6', '7ea663a5-f4d9-4da5-91fb-ef4b43334115', 'Student', 'STUDENT')) 
        AS S ([Id], [ConcurrencyStamp], [Name], [NormalizedName])) AS Source
ON Target.Id = Source.Id
WHEN NOT MATCHED THEN
    INSERT ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
    VALUES(Source.Id, Source.[ConcurrencyStamp], Source.[Name], Source.[NormalizedName]);