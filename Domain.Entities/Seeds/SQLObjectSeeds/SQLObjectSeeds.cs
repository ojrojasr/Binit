namespace Domain.Entities.Seeds.SQLObjectSeeds
{
    public class SQLObjectSeeds
    {
        public const string SpCreateCategory = @"IF OBJECT_ID('dbo.SP_CreateCategories') IS NULL
            BEGIN
                EXECUTE('CREATE PROCEDURE [dbo].[SP_CreateCategories]
                @UserId uniqueidentifier,
                @Name varchar(250),
				@Description varchar(250)
                AS
                insert into category ([Id], [Deleted], [CreatedDate], [LastEditedDate], [LastEditorId], [CreatorId], [Name], [Description])
                values(NEWID(), 0, GETDATE(), GETDATE(), @UserId, @UserId, @Name, @Description)
                select Id, Name, Description from Category
                ' )
            END";

        public const string ViewGetProductFeaturesCount = @"IF OBJECT_ID('dbo.View_ProductFeaturesCounts') IS NULL
            BEGIN
                EXECUTE('CREATE VIEW dbo.View_ProductFeaturesCounts
                AS 
                SELECT p.Name as Name, 
                p.Description as Description,
                Count(f.Id) as FeatureCount 
                FROM Product p JOIN Feature f on f.ProductId = p.Id
                Where p.Deleted = 0 and f.Deleted = 0
                GROUP BY p.Name, p.Description' )
            END";
    }
}
