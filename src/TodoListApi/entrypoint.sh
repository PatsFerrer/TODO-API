#!/bin/bash

# Inicia o SQL Server em background
/opt/mssql/bin/sqlservr &

# Guarda o ID do processo do SQL Server
SQL_SERVER_PID=$!

# Espera o SQL Server ficar pronto, testando a conexao
echo "Aguardando o SQL Server iniciar..."
for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SQL_SA_PASSWORD}" -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]
    then
        echo "SQL Server pronto!"
        # Roda o script de inicializacao para criar o DB e as tabelas
        echo "Rodando o script InitDb.sql..."
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SQL_SA_PASSWORD}" -i /docker-entrypoint-initdb.d/InitDb.sql
        break
    else
        echo "Ainda nao esta pronto... tentando de novo em 1s"
        sleep 1
    fi
done

# Espera o processo do SQL Server terminar (para manter o container rodando)
wait $SQL_SERVER_PID