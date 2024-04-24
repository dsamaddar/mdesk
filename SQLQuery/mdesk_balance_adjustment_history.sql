
create function fnGetWarehouseNmByID(@WarehouseID nvarchar(50))
returns nvarchar(50)
as
begin
	Declare @WarehouseName as nvarchar(50) set @WarehouseName = '';

	if exists(select WarehouseName from tblWarehouse where WarehouseID = @WarehouseID)
	begin
		Select @WarehouseName = WarehouseName from tblWarehouse where WarehouseID = @WarehouseID;
	end
	else
		set @WarehouseName = @WarehouseID;

	return isnull(@WarehouseName,'');
end

GO

-- exec spGetItemBalanceAdjHist 'WH-00000002','ITM-00000113','01-Jan-2024','31-Dec-24'
create proc spGetItemBalanceAdjHist
@WarehouseID nvarchar(50),
@ItemID nvarchar(50),
@StartDate date,
@EndDate date
as
begin
	declare @tbl_adj_hist as table(
	SLNo int identity(1,1),
	WarehouseID nvarchar(50),
	ItemID nvarchar(50),
	TransferEvent nvarchar(50),
	TransferDt datetime,
	Src nvarchar(50),
	Dest nvarchar(50),
	Credit int,
	Debit int,
	Balance int,
	Taken bit default 0
	);

	insert into @tbl_adj_hist(WarehouseID,ItemID,TransferEvent,TransferDt,Src,Dest,Credit,Debit,Balance)
	select WarehouseID,ItemID,TransferType,EntryDate,TransferedRef,WarehouseID,ItemBalance,0,0
	from tblWarehouseItems where ItemID = @ItemID and WarehouseID=@WarehouseID
	and EntryDate between @StartDate and @EndDate
	UNION ALL
	select @WarehouseID,@ItemID,'Despatch',AdjustmentDate,@WarehouseID,RequisitionID,0,AdjustedBalance,0
	from tblWarehouseAdjHistory where WarehouseItemID in (
	select WarehouseItemID from tblWarehouseItems where ItemID = @ItemID and WarehouseID=@WarehouseID
	and EntryDate between @StartDate and @EndDate
	)
	order by EntryDate;

	Declare @Count as int set @Count = 0;
	Declare @NCount as int set @NCount = 0;
	Declare @Debit as int set @Debit = 0;
	Declare @Credit as int set @Credit = 0;
	Declare @Balance as int set @Balance = 0;
	Declare @SLNo as int Set @SLNo = 0;

	select @NCount = count(*) from @tbl_adj_hist;

	While @Count < @NCount
	begin
		select top 1 @SLNo = SLNo, @Debit = Debit, @Credit = Credit from @tbl_adj_hist where Taken = 0;

		set @Balance = @Balance + (@Credit - @Debit);
		--print 'SL: ' + convert(nvarchar,@SLNo) + 'C: ' + convert(nvarchar,@Credit) + ' D: '+ convert(nvarchar,@Debit) + ' B: ' + convert(nvarchar,@Balance);

		update @tbl_adj_hist set Balance = @Balance, Taken = 1 where SLNo = @SLNo;

		set @Count += 1;
		set @Debit = 0;
		set @Credit = 0;
	end

	select TransferEvent,TransferDt,dbo.fnGetWarehouseNmByID(Src) Src,dbo.fnGetWarehouseNmByID(Dest) Dest,
	Credit,Debit,Balance from @tbl_adj_hist;
end

GO