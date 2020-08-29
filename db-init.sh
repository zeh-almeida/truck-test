#!/bin/bash

sleep 10s

TRIES=10
DBSTATUS=1
ERRCODE=1
COUNT=0

echo "[SQLSETUP] Checking DB"

while [[ $COUNT -lt $TRIES ]] ; do
	((COUNT=COUNT+1))
	echo "[SQLSETUP] trying time: $COUNT"

	DBSTATUS=$(/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases")
	ERRCODE=$?
	sleep 1

	if [[ $DBSTATUS -eq 0 ]] && [[ $ERRCODE -eq 0 ]]; then
		break
	fi
done

if [[ $DBSTATUS -ne 0 ]] || [[ $ERRCODE -ne 0 ]]; then
	echo "[SQLSETUP] SQL Server took more than $COUNT tries to start up or one or more databases are not in an ONLINE state"
	exit 1
fi

sleep 2

echo "[SQLSETUP] Initialize DB"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -i db-init.sql