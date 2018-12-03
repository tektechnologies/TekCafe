echo off

rem batch file to run a script to create a db
rem 11/01/2018

sqlcmd -S localhost -E -i TekCafe_DB_PM.sql
rem sqlcmd -S localhost\sqlexpress -E -i TekCafe_DB_PM.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE