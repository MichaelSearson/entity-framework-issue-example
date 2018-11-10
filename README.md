Commands runs:

```text
> Add-Migration InitialCreate
> Update-Database
```

Profiling the database shows me the following queries (formatted for readability) being executed when I call `context.SaveChanges()`:

```sql
exec sp_executesql N'SET NOCOUNT ON;

INSERT INTO [Accounts] 
(
    [AccountId], 
    [AddedByAccountId], 
    [AddedOnUtc], 
    [ModifiedByAccountId], 
    [ModifiedOnUtc]
)
VALUES 
(
    @p0, 
    @p1, 
    @p2, 
    @p3, 
    @p4
);
',N'@p0 uniqueidentifier,
@p1 uniqueidentifier,
@p2 datetime2(7),
@p3 uniqueidentifier,
@p4 datetime2(7)',

@p0='35C38DF0-A959-4232-AADD-40DB2260F557',
@p1=NULL,
@p2='2018-11-10 14:29:25.5361634',
@p3=NULL,
@p4='2018-11-10 14:29:25.5362510' 
```

So far this is exactly what I expect based on the C# I provided:

```csharp
var systemAccount = new Account
{
    AccountId = Guid.Parse("35c38df0-a959-4232-aadd-40db2260f557"),
    AddedByAccountId = null,
    AddedOnUtc = DateTime.UtcNow,
    ModifiedByAccountId = null,
    ModifiedOnUtc = DateTime.UtcNow
};
```

However, when I provide a value for `AddedByAccountId` to the successive inserts I still get a NULL value in the generated SQL  (again formatted for readability):

```sql
exec sp_executesql N'SET NOCOUNT ON;

INSERT INTO [Accounts] 
(
    [AccountId], 
    [AddedByAccountId], 
    [AddedOnUtc], 
    [ModifiedByAccountId], 
    [ModifiedOnUtc]
)
VALUES 
(
    @p5, 
    @p6, 
    @p7, 
    @p8, 
    @p9
),
(
    @p10, 
    @p11, 
    @p12, 
    @p13, 
    @p14
),
(
    @p15, 
    @p16, 
    @p17, 
    @p18, 
    @p19
),
(
    @p20, 
    @p21, 
    @p22, 
    @p23, 
    @p24
);
',N'@p5 uniqueidentifier,
@p6 uniqueidentifier,
@p7 datetime2(7),
@p8 uniqueidentifier,
@p9 datetime2(7),
@p10 uniqueidentifier,
@p11 uniqueidentifier,
@p12 datetime2(7),
@p13 uniqueidentifier,
@p14 datetime2(7),
@p15 uniqueidentifier,
@p16 uniqueidentifier,
@p17 datetime2(7),
@p18 uniqueidentifier,
@p19 datetime2(7),
@p20 uniqueidentifier,
@p21 uniqueidentifier,
@p22 datetime2(7),
@p23 uniqueidentifier,
@p24 datetime2(7)',
@p5='015B76FC-2833-45D9-85A7-AB1C389C1C11',
@p6=NULL,
@p7='2018-11-10 14:29:25.5363017',
@p8='35C38DF0-A959-4232-AADD-40DB2260F557',
@p9='2018-11-10 14:29:25.5363022',
@p10='538EE0DD-531A-41C6-8414-0769EC5990D8',
@p11=NULL,
@p12='2018-11-10 14:29:25.5363031',
@p13='35C38DF0-A959-4232-AADD-40DB2260F557',
@p14='2018-11-10 14:29:25.5363034',
@p15='8288D9AC-FBCE-417E-89EF-82266B284B78',
@p16=NULL,
@p17='2018-11-10 14:29:25.5363039',
@p18='35C38DF0-A959-4232-AADD-40DB2260F557',
@p19='2018-11-10 14:29:25.5363042',
@p20='4BCFE9F8-E4A5-49F0-B6EE-44871632A903',
@p21='35C38DF0-A959-4232-AADD-40DB2260F557',
@p22='2018-11-10 14:29:25.5363047',
@p23='35C38DF0-A959-4232-AADD-40DB2260F557',
@p24='2018-11-10 14:29:25.5363047'
```

From the above output you'll see that only the Account with the Id `4BCFE9F8-E4A5-49F0-B6EE-44871632A903` had its `AddedByAccountId` value set properly. The rest ended up with `NULL` literals.

Looking in SQL Server I can see that the migration script has applied properly - the `IX_Accounts_AddedByAccountId` and `IX_Accounts_ModifiedByAccountId` are both non-unique, non-clusetered indexes so I'm at a bit of a loss what's causing this data conversion.

Running this SQL manually in a SSMS query window and replacing the `NULL` values with the intended GUID works fine!