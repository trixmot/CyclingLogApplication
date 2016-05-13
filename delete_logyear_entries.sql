DELETE FROM Table_Ride_Information
WHERE LogYearID=1

DBCC CHECKIDENT ('[Table_Ride_Information]', RESEED, 0);

DELETE FROM Table_Log_year
WHERE LogYearID=1

DBCC CHECKIDENT ('[Table_Log_year]', RESEED, 0);


DELETE FROM Table_Log_year
WHERE LogYearID=5

DBCC CHECKIDENT ('[Table_Log_year]', RESEED, 4);