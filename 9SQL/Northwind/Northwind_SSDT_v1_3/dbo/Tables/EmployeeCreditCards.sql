CREATE TABLE [dbo].[EmployeeCreditCards] (
    [Id]             INT          NOT NULL,
    [NumberCard]     INT          NULL,
    [ExpirationDate] DATETIME     NULL,
    [OwnersName]     VARCHAR (30) NULL,
    [EmployeeId]     INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

