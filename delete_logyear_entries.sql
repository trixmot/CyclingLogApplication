DELETE FROM Table_Ride_Information
WHERE LogYearID=1

DBCC CHECKIDENT ('[Table_Ride_Information]', RESEED, 0);