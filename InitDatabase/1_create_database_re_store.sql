SELECT 'CREATE DATABASE re_store'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 're_store')\gexec