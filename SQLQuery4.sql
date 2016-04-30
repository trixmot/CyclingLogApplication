GO
DECLARE @LogYearID bigint
DECLARE @WeekNumber bigint

SET @LogYearID=1
SET @WeekNumber=11
SET @CurrentWeekCount=12

SELECT SUM(RideDistance) 
FROM Table_Ride_Information 
WHERE @LogYearID=[LogYearID]


SELECT RideDistance 
FROM Table_Ride_Information 
WHERE @WeekNumber=[WeekNumber]