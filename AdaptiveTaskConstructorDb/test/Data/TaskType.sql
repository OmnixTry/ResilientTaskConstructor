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

MERGE [test].[TaskType] AS Target
USING (
    SELECT * 
    FROM (VALUES 
        (1, 'MultipleChoice'),
        (2, 'OpenBrackets'),
        (3, 'General')) AS S ([Id], [Name])) AS Source
ON Target.Id = Source.Id
WHEN NOT MATCHED THEN
    INSERT (Id, [Name])
    VALUES(Source.Id, Source.[Name]);