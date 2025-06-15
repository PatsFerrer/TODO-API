#!/bin/bash

# Inicia o SQL Server em background
/opt/mssql/bin/sqlservr &

# Guarda o ID do processo do SQL Server
SQL_SERVER_PID=$!

echo "Aguardando o SQL Server iniciar... (pode levar ate um minuto)"

# Espera o SQL Server ficar pronto, testando a conexao em um loop
for i in {1..60};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SQL_SA_PASSWORD}" -Q "SELECT 1" &>/dev/null
    if [ $? -eq 0 ]
    then
        echo "SQL Server pronto!"
        echo "Rodando o script InitDb.sql para criar o banco e as tabelas..."
        /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "${SQL_SA_PASSWORD}" -i /docker-entrypoint-initdb.d/InitDb.sql
        echo "Script finalizado."
        break
    else
        echo "Tentativa $i: Nao esta pronto... tentando de novo em 2s"
        sleep 2
    fi
done

echo "Inicializacao finalizada. Mantendo o container no ar."
# Espera o processo do SQL Server terminar (para manter o container rodando)
wait $SQL_SERVER_PID