RESTORE DATABASE RentalHouseFinding
FROM DISK = 'C:\Users\Nambaby\Desktop\Backup_122_posts_ngay13_04_Nam.bak'
WITH REPLACE,
MOVE 'RentalHouseFinding' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\nh5ho1jp_rentalhousefinding_RentalHouseFinding_7b6e752926dd4ca69975b506b4e57040.mdf'
,MOVE 'RentalHouseFinding_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\nh5ho1jp_rentalhousefinding_RentalHouseFinding_log_749a57c18a3a4ff0b762635d6c62f8bd.ldf'